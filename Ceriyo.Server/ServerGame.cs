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

namespace Ceriyo.Server
{
    public class ServerGame : Microsoft.Xna.Framework.Game
    {
        public event EventHandler<ServerStatusUpdateEventArgs> OnSignalGUIUpdate;
        private ServerGameStatus GameStatus { get; set; }
        private ServerStatusUpdateEventArgs EventArguments { get; set; }
        private float SignalGUIUpdateTimer { get; set; }
        private const float SignalGUIUpdateSeconds = 2.0f;
        public Queue<ServerGUIStatus> GUIStatusUpdateQueue
        {
            get;
            private set;
        }
        private ServerSettings Settings { get; set; }
        private bool IsServerRunning { get; set; }

        public ServerGame()
        {
            GameStatus = new ServerGameStatus();
            EventArguments = new ServerStatusUpdateEventArgs();
            GUIStatusUpdateQueue = new Queue<ServerGUIStatus>();
        }

        protected override void Initialize()
        {
            FlatRedBallServices.InitializeCommandLine(this);
            base.Initialize();

            Form gameForm = (Form)Form.FromHandle(this.Window.Handle);
            gameForm.Opacity = 0;
            gameForm.ShowInTaskbar = false;
        }

        protected override void Update(GameTime gameTime)
        {
            FlatRedBallServices.UpdateCommandLine(gameTime);
            
            ScreenManager.Activity();

            base.Update(gameTime);
            SuppressDraw();

            SignalGUIUpdateTimer += TimeManager.SecondDifference;

            if (SignalGUIUpdateTimer >= SignalGUIUpdateSeconds)
            {
                if (OnSignalGUIUpdate != null)
                {
                    EventArguments.GameStatus = GameStatus;

                    OnSignalGUIUpdate(this, EventArguments);
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
                ServerGUIStatus status = GUIStatusUpdateQueue.Dequeue();
                this.Settings = status.Settings;
                this.IsServerRunning = status.IsServerRunning;
            }
        }
    }
}
