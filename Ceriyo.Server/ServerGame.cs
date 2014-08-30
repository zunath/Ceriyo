using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlatRedBall;
using FlatRedBall.Screens;
using Microsoft.Xna.Framework;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.Server;
using Ceriyo.Data.Settings;
using System.ComponentModel;
using System.Collections.Concurrent;

namespace Ceriyo.Server
{
    public class ServerGame : Microsoft.Xna.Framework.Game
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

        public ServerGame(ServerStartupArgs args)
        {
            GUIStatusUpdateQueue = new ConcurrentQueue<ServerGUIStatus>();
            NetworkManager = new ServerNetworkManager(args.ServerPassword, args.Port);
        }

        protected override void Initialize()
        {
            if (!FlatRedBallServices.IsInitialized)
            {
                FlatRedBallServices.InitializeCommandLine(this);
            }
            base.Initialize();

            Form gameForm = (Form)Form.FromHandle(this.Window.Handle);
            gameForm.Opacity = 0;
            gameForm.ShowInTaskbar = false;

            if (OnGameStarting != null)
            {
                OnGameStarting(this, new EventArgs());
            }
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
                    ServerStatusUpdateEventArgs e = new ServerStatusUpdateEventArgs();
                    e.ConnectedUsernames = NetworkManager.GetPlayerNames();

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
            return;
        }

        private void ProcessGUIStatusUpdates()
        {
            while (GUIStatusUpdateQueue.Count > 0)
            {
                ServerGUIStatus status;
                if (GUIStatusUpdateQueue.TryDequeue(out status))
                {
                    this.Settings = status.Settings;
                    this.IsServerRunning = status.IsServerRunning;

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
            base.OnExiting(sender, args);

            if (OnGameExiting != null)
            {
                OnGameExiting(this, new EventArgs());
            }
        }
    }
}
