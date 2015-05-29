using System.ComponentModel;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Server;
using Ceriyo.Data.Settings;
using Ceriyo.Library.ScriptEngine;
using FlatRedBall;
using FlatRedBall.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Ceriyo.Data.Engine;
using Ceriyo.Data.ResourceObjects;
using Ceriyo.Library.Network;
using Ceriyo.Library.Network.Packets;
using Lidgren.Network;

namespace Ceriyo.Server
{
    public class ServerGame : Game
    {
        #region Properties

        public event EventHandler<EventArgs> OnGameStarting;
        public event EventHandler<EventArgs> OnGameExiting;
        public event EventHandler<ServerStatusUpdateEventArgs> OnSignalGUIUpdate;

        private float SignalGUIUpdateTimer { get; set; }
        private const float SignalGUIUpdateSeconds = 2.0f;
        public ConcurrentQueue<ServerGUIStatus> GUIStatusUpdateQueue { get; private set; }
        private Dictionary<NetConnection, ServerPlayer> Players { get; set; }
        private ServerSettings Settings { get; set; }
        private bool IsServerRunning { get; set; }
        private ServerScriptData ScriptData { get; set; }
        private GameModule Module { get; set; }
        private BindingList<Area> Areas { get; set; }
        private BindingList<Item> Items { get; set; }

        #endregion

        #region Game Loop

        public ServerGame(ServerStartupArgs args)
        {
            Players = new Dictionary<NetConnection, ServerPlayer>();
            GUIStatusUpdateQueue = new ConcurrentQueue<ServerGUIStatus>();

            if (ModuleDataManager.LoadModule(args.ModuleFileName, true) != FileOperationResultType.Success)
            {
                throw new Exception("Server was unable to load module.");
            }

            Module = WorkingDataManager.GetGameModule();
            Areas = WorkingDataManager.GetAllGameObjects<Area>(ModulePaths.AreasDirectory);
            Items = WorkingDataManager.GetAllGameObjects<Item>(ModulePaths.ItemsDirectory);

            Settings = new ServerSettings
            {
                Port = args.Port,
                PlayerPassword = args.ServerPassword
            };

            ScriptData = new ServerScriptData
            {
                Areas = Areas,
                Items = Items,
                Levels = Module.Levels
            };
        }

        protected override void Initialize()
        {
            if (!FlatRedBallServices.IsInitialized)
            {
                FlatRedBallServices.InitializeCommandLine();
            }
            base.Initialize();

            Form gameForm = (Form)Control.FromHandle(Window.Handle);
            gameForm.Opacity = 0;
            gameForm.ShowInTaskbar = false;

            NetworkManager.Initialize(NetworkAgentRole.Server, Settings.Port);
            BindEvents();
            SubscribePacketActions();

            ScriptManager.RunModuleScript(Module.Scripts[ScriptEventType.OnModuleLoad], Module);

            if (OnGameStarting != null)
            {
                OnGameStarting(this, new EventArgs());
            }
        }

        private void BindEvents()
        {
            NetworkManager.OnConnected += Agent_OnConnected;
            NetworkManager.OnDisconnected += Agent_OnDisconnected;
            NetworkManager.OnDisconnecting += Agent_OnDisconnecting;
        }

        protected override void Update(GameTime gameTime)
        {
            FlatRedBallServices.UpdateCommandLine(gameTime);
            ScreenManager.Activity();

            base.Update(gameTime);
            NetworkManager.Update();
            SuppressDraw();
            UpdateScriptManager();

            SignalGUIUpdateTimer += TimeManager.SecondDifference;
            if (SignalGUIUpdateTimer >= SignalGUIUpdateSeconds)
            {
                if (OnSignalGUIUpdate != null)
                {
                    ServerStatusUpdateEventArgs e = new ServerStatusUpdateEventArgs
                    {
                        ConnectedUsernames = GetPlayerNames()
                    };

                    OnSignalGUIUpdate(this, e);
                }

                // Every few seconds, the GUI thread enqueues a new object containing current values for a number of fields.
                // We need to process those updates here.
                ProcessGUIStatusUpdates();
                
                SignalGUIUpdateTimer = 0.0f;
            }
        }

        private void UpdateScriptManager()
        {
            ScriptData.Areas = Areas;
            ScriptData.Items = Items;
            ScriptData.Levels = Module.Levels;
            ScriptManager.Update(ScriptData);
        }

        protected override void Draw(GameTime gameTime)
        {
        }

        private void ProcessGUIStatusUpdates()
        {
            while (GUIStatusUpdateQueue.Count > 0)
            {
                ServerGUIStatus status;
                if (GUIStatusUpdateQueue.TryDequeue(out status))
                {
                    Settings = status.Settings;
                    IsServerRunning = status.IsServerRunning;

                    if (!IsServerRunning)
                    {
                        Exit();
                    }
                }
            }
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            UnsubscribePacketActions();
            NetworkManager.Shutdown();
            ModuleDataManager.CloseModule();
            base.OnExiting(sender, args);

            if (OnGameExiting != null)
            {
                OnGameExiting(this, new EventArgs());
            }
        }

        #endregion

        #region Network Related

        private BindingList<string> GetPlayerNames()
        {
            return new BindingList<string>((from pc
                                            in Players.Values
                                            select pc.Username).ToList());
        }

        private void Agent_OnConnected(object sender, ConnectionStatusEventArgs e)
        {
            UserInfoPacket packet = new UserInfoPacket
            {
                IsRequest = true
            };

            packet.Send(NetDeliveryMethod.ReliableUnordered, e.Connection);
        }

        private void Agent_OnDisconnecting(object sender, ConnectionStatusEventArgs e)
        {
            ScriptManager.RunModuleScript(Module.Scripts[ScriptEventType.OnModulePlayerLeaving], Players[e.Connection]);
        }

        private void Agent_OnDisconnected(object sender, ConnectionStatusEventArgs e)
        {
            ScriptManager.RunModuleScript(Module.Scripts[ScriptEventType.OnModulePlayerLeft], Players[e.Connection]);

            if (Players.ContainsKey(e.Connection))
            {
                Players.Remove(e.Connection);
            }
        }

        #endregion

        #region Packet Handling

        private void SubscribePacketActions()
        {
            NetworkManager.SubscribePacketAction(typeof(CharacterCreationScreenPacket), ReceiveCharacterCreationScreenPacket);
            NetworkManager.SubscribePacketAction(typeof(CharacterSelectionScreenPacket), ReceiveCharacterSelectionScreenPacket);
            NetworkManager.SubscribePacketAction(typeof(CreateCharacterPacket), ReceiveCreateCharacterPacket);
            NetworkManager.SubscribePacketAction(typeof(DeleteCharacterPacket), ReceiveDeleteCharacterPacket);
            NetworkManager.SubscribePacketAction(typeof(SelectCharacterPacket), ReceiveSelectCharacterPacket);
            NetworkManager.SubscribePacketAction(typeof(UserInfoPacket), ReceiveUserInfoPacket);
            NetworkManager.SubscribePacketAction(typeof(GameScreenInitPacket), ReceiveGameInitPacket);
        }

        private void UnsubscribePacketActions()
        {
            NetworkManager.UnsubscribePacketAction(typeof(CharacterCreationScreenPacket), ReceiveCharacterCreationScreenPacket);
            NetworkManager.UnsubscribePacketAction(typeof(CharacterSelectionScreenPacket), ReceiveCharacterSelectionScreenPacket);
            NetworkManager.UnsubscribePacketAction(typeof(CreateCharacterPacket), ReceiveCreateCharacterPacket);
            NetworkManager.UnsubscribePacketAction(typeof(DeleteCharacterPacket), ReceiveDeleteCharacterPacket);
            NetworkManager.UnsubscribePacketAction(typeof(SelectCharacterPacket), ReceiveSelectCharacterPacket);
            NetworkManager.UnsubscribePacketAction(typeof(UserInfoPacket), ReceiveUserInfoPacket);
            NetworkManager.UnsubscribePacketAction(typeof(GameScreenInitPacket), ReceiveGameInitPacket);
        }

        private void ReceiveCharacterCreationScreenPacket(PacketBase packetBase)
        {
            CharacterCreationScreenPacket response = new CharacterCreationScreenPacket
            {
                Abilities = WorkingDataManager.GetAllGameObjects<Ability>(ModulePaths.AbilitiesDirectory).ToList(),
                CharacterClasses = WorkingDataManager.GetAllGameObjects<CharacterClass>(ModulePaths.CharacterClassesDirectory).ToList(),
                Skills = WorkingDataManager.GetAllGameObjects<Skill>(ModulePaths.SkillsDirectory).ToList()
            };

            response.Send(NetDeliveryMethod.ReliableUnordered, packetBase.SenderConnection);
        }

        private void ReceiveCharacterSelectionScreenPacket(PacketBase packetBase)
        {
            string username = Players[packetBase.SenderConnection].Username;
            List<Player> characters = EngineDataManager.GetPlayers(username);

            CharacterSelectionScreenPacket response = new CharacterSelectionScreenPacket
            {
                CharacterList = characters,
                Announcement = Settings.Announcement,
                CanDeleteCharacters = Settings.AllowCharacterDeletion
            };

            response.Send(NetDeliveryMethod.ReliableUnordered, packetBase.SenderConnection);
            
        }

        private void ReceiveCreateCharacterPacket(PacketBase packetBase)
        {
            CreateCharacterPacket packet = packetBase as CreateCharacterPacket;
            if (packet == null) return;

            Player pc = new Player
            {
                Name = packet.Name,
                Description = packet.Description
            };

            string username = Players[packet.SenderConnection].Username;
            EngineDataManager.SavePlayer(username, pc, true);

            CreateCharacterPacket response = new CreateCharacterPacket
            {
                ResponsePlayer = pc
            };

            response.Send(NetDeliveryMethod.ReliableUnordered, packet.SenderConnection);
        }

        private void ReceiveDeleteCharacterPacket(PacketBase packetBase)
        {
            DeleteCharacterPacket packet = packetBase as DeleteCharacterPacket;
            if (packet == null) return;

            bool success = false;

            if (Settings.AllowCharacterDeletion)
            {
                success = EngineDataManager.DeletePlayer(Players[packetBase.SenderConnection].Username, packet.CharacterResref);
            }

            DeleteCharacterPacket response = new DeleteCharacterPacket
            {
                IsDeleteSuccessful = success
            };

            response.Send(NetDeliveryMethod.ReliableUnordered, packetBase.SenderConnection);

        }

        private void ReceiveSelectCharacterPacket(PacketBase packetBase)
        {
            SelectCharacterPacket packet = packetBase as SelectCharacterPacket;
            if (packet == null) return;

            string username = Players[packet.SenderConnection].Username;
            Player pc = EngineDataManager.GetPlayer(username, packet.Resref);

            SelectCharacterPacket response = new SelectCharacterPacket();

            if (pc != null)
            {
                Players[packet.SenderConnection].PC = pc;
                response.IsSuccessful = true;
            }

            response.Send(NetDeliveryMethod.ReliableUnordered, packet.SenderConnection);
        }

        private void ReceiveUserInfoPacket(PacketBase packetBase)
        {
            UserInfoPacket packet = packetBase as UserInfoPacket;
            if (packet == null) return;

            if (!Players.ContainsKey(packet.SenderConnection) &&
                Players.SingleOrDefault(x => x.Value.Username == packet.Username).Value == null)
            {
                ServerPlayer pc = new ServerPlayer
                {
                    PC = new Player(),
                    Username = packet.Username
                };

                Players.Add(packet.SenderConnection, pc);

                if (!Directory.Exists(EnginePaths.CharactersDirectory + packet.Username))
                {
                    Directory.CreateDirectory(EnginePaths.CharactersDirectory + packet.Username);
                }

                UserConnectedPacket response = new UserConnectedPacket
                {
                    IsSuccessful = true
                };

                response.Send(NetDeliveryMethod.ReliableUnordered, packet.SenderConnection);
            }

        }

        private void ReceiveGameInitPacket(PacketBase packetBase)
        {
            GameScreenInitPacket packet = packetBase as GameScreenInitPacket;
            if (packet == null) return;

            var player = Players[packet.SenderConnection];
            ScriptManager.RunModuleScript(Module.Scripts[ScriptEventType.OnModulePlayerEnter], player);

            GameResource tilesetGraphicResource = Areas[0].AreaTileset.Graphic;
            GameScreenInitPacket response = new GameScreenInitPacket
            {
                AreaDescription = Areas[0].Description,
                AreaName = Areas[0].Name,
                AreaResref = Areas[0].Resref,
                AreaTag = Areas[0].Tag,
                IsRequest = false,
                AreaTiles = Areas[0].MapTiles,
                AreaLayers = Areas[0].LayerCount,
                TilesetGraphicResourceFileName = tilesetGraphicResource.FileName,
                TilesetGraphicResourcePackage = tilesetGraphicResource.Package
            };

            response.Send(NetDeliveryMethod.ReliableUnordered, packet.SenderConnection);
            
        }

        #endregion

    }
}
