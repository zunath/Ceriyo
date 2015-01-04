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
using System.Linq;
using System.Windows.Forms;
using Ceriyo.Data.Engine;
using Ceriyo.Library.Global;
using Ceriyo.Library.Network;
using Ceriyo.Library.Network.Packets;
using Lidgren.Network;

namespace Ceriyo.Server
{
    public class ServerGame : Game
    {
        #region Properties

        private NetworkTransferData TransferData { get; set; }
        public event EventHandler<EventArgs> OnGameStarting;
        public event EventHandler<EventArgs> OnGameExiting;
        public event EventHandler<ServerStatusUpdateEventArgs> OnSignalGUIUpdate;

        private float SignalGUIUpdateTimer { get; set; }
        private const float SignalGUIUpdateSeconds = 2.0f;
        public ConcurrentQueue<ServerGUIStatus> GUIStatusUpdateQueue
        {
            get;
            private set;
        }
        private Dictionary<NetConnection, ServerPlayer> Players { get; set; }
        private ServerSettings Settings { get; set; }
        private bool IsServerRunning { get; set; }
        private ScriptManager Scripts { get; set; }
        private BindingList<Area> Areas { get; set; }
        private GameModule Module { get; set; }

        #endregion

        #region Game Loop

        public ServerGame(ServerStartupArgs args)
        {
            Players = new Dictionary<NetConnection, ServerPlayer>();
            GUIStatusUpdateQueue = new ConcurrentQueue<ServerGUIStatus>();
            Scripts = new ScriptManager();

            if (ModuleDataManager.LoadModule(args.ModuleFileName, true) != FileOperationResultTypeEnum.Success)
            {
                throw new Exception("Server was unable to load module.");
            }

            Module = WorkingDataManager.GetGameModule();
            Areas = WorkingDataManager.GetAllGameObjects<Area>(ModulePaths.AreasDirectory);

            Settings = new ServerSettings
            {
                Port = args.Port,
                PlayerPassword = args.ServerPassword
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

            CeriyoServices.Initialize(NetworkAgentRoleEnum.Server, Settings.Port);
            BindEvents();

            if (OnGameStarting != null)
            {
                OnGameStarting(this, new EventArgs());
            }
        }

        private void BindEvents()
        {
            CeriyoServices.OnPacketReceived += ProcessPacket;
            CeriyoServices.Agent.OnConnected += Agent_OnConnected;
            CeriyoServices.Agent.OnDisconnected += Agent_OnDisconnected;
            CeriyoServices.Agent.OnDisconnecting += Agent_OnDisconnecting;
        }

        protected override void Update(GameTime gameTime)
        {
            FlatRedBallServices.UpdateCommandLine(gameTime);
            
            ScreenManager.Activity();

            base.Update(gameTime);
            UpdateNetworkTransferData();
            CeriyoServices.Update();
            SuppressDraw();

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
            CeriyoServices.OnPacketReceived -= ProcessPacket;
            CeriyoServices.Agent.Shutdown();
            ModuleDataManager.CloseModule();
            base.OnExiting(sender, args);

            if (OnGameExiting != null)
            {
                OnGameExiting(this, new EventArgs());
            }
        }

        #endregion

        #region Network Related

        private void UpdateNetworkTransferData()
        {
            TransferData = new NetworkTransferData
            {
                Players = Players,
                Settings = Settings,
                SelectedArea = Areas[0]
            };
        }

        private void ProcessPacket(object sender, PacketEventArgs e)
        {
            TransferData = e.Packet.ServerReceive(TransferData);
        }


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

        }

        private void Agent_OnDisconnected(object sender, ConnectionStatusEventArgs e)
        {
            if (Players.ContainsKey(e.Connection))
            {
                Players.Remove(e.Connection);
            }
        }

        #endregion
    }
}
