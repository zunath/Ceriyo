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

namespace Ceriyo.Server
{
    public class ServerGame : Microsoft.Xna.Framework.Game
    {
        public event EventHandler<ServerStatusUpdateEventArgs> OnSignalGUIUpdate;
        private ServerGameStatus GameStatus { get; set; }
        private ServerGUIStatus GUIStatus { get; set; }
        private ServerStatusUpdateEventArgs EventArguments { get; set; }
        private float SignalGUIUpdateTimer { get; set; }
        private const float SignalGUIUpdateSeconds = 2.0f;

        public ServerGame()
        {
            GameStatus = new ServerGameStatus();
            GUIStatus = new ServerGUIStatus();
            EventArguments = new ServerStatusUpdateEventArgs();
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
                    EventArguments.GUIStatus = GUIStatus;

                    OnSignalGUIUpdate(this, EventArguments);

                    EventArguments.GameStatus = null;
                    EventArguments.GUIStatus = null;
                }

                SignalGUIUpdateTimer = 0.0f;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            return;
        }
    }
}
