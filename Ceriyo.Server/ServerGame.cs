using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Documents;
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Packets;
using Ceriyo.Data.Server;
using Ceriyo.Data.Settings;
using Ceriyo.Library.ScriptEngine;
using FlatRedBall;
using FlatRedBall.Screens;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Concurrent;
using System.Windows.Forms;

namespace Ceriyo.Server
{
    public class ServerGame : Game
    {
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
        private ServerSettings Settings { get; set; }
        private bool IsServerRunning { get; set; }
        private ServerNetworkManager NetworkManager { get; set; }
        private ScriptManager Scripts { get; set; }
        private BindingList<Area> Areas { get; set; }
        private GameModule Module { get; set; }

        public ServerGame(ServerStartupArgs args)
        {
            GUIStatusUpdateQueue = new ConcurrentQueue<ServerGUIStatus>();
            NetworkManager = new ServerNetworkManager(args.ServerPassword, args.Port);
            Scripts = new ScriptManager();

            if (ModuleDataManager.LoadModule(args.ModuleFileName, true) != FileOperationResultTypeEnum.Success)
            {
                throw new Exception("Server was unable to load module.");
            }

            Module = WorkingDataManager.GetGameModule();
            Areas = WorkingDataManager.GetAllGameObjects<Area>(ModulePaths.AreasDirectory);
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

            if (OnGameStarting != null)
            {
                OnGameStarting(this, new EventArgs());
            }

            NetworkManager.OnPacketReceived += NetworkManager_OnPacketReceived;

        }

        protected override void Update(GameTime gameTime)
        {
            FlatRedBallServices.UpdateCommandLine(gameTime);
            
            ScreenManager.Activity();

            base.Update(gameTime);
            NetworkManager.Update();
            SuppressDraw();

            SignalGUIUpdateTimer += TimeManager.SecondDifference;
            if (SignalGUIUpdateTimer >= SignalGUIUpdateSeconds)
            {
                if (OnSignalGUIUpdate != null)
                {
                    ServerStatusUpdateEventArgs e = new ServerStatusUpdateEventArgs
                    {
                        ConnectedUsernames = NetworkManager.GetPlayerNames()
                    };

                    OnSignalGUIUpdate(this, e);
                }

                // Every few seconds, the GUI thread enqueues a new object containing current values for a number of fields.
                // We need to process those updates here.
                ProcessGUIStatusUpdates();
                
                // Refresh the network manager while we're at it.
                NetworkManager.RefreshSettings(Settings);

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
            NetworkManager.Destroy();
            ModuleDataManager.CloseModule();
            base.OnExiting(sender, args);

            if (OnGameExiting != null)
            {
                OnGameExiting(this, new EventArgs());
            }
        }

        #region Packet Processing

        private void NetworkManager_OnPacketReceived(object sender, PacketEventArgs e)
        {
            PacketBase packet = e.Packet;
            Type type = packet.GetType();

            if (type == typeof (EnteringGameScreenPacket))
            {
                ReceiveEnteringGameScreenPacket(packet as EnteringGameScreenPacket);
            }
        }


        private void ReceiveEnteringGameScreenPacket(EnteringGameScreenPacket packet)
        {
            EnteringGameScreenPacket response = new EnteringGameScreenPacket
            {
                
            };

            NetworkManager.SendPacket(response, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
        }

        #endregion
    }
}
