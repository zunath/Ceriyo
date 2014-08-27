using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlatRedBall;
using FlatRedBall.Screens;
using Microsoft.Xna.Framework;

namespace Ceriyo.Server
{
    public class ServerGame : Microsoft.Xna.Framework.Game
    {
        public event EventHandler<EventArgs> OnUpdateComplete;

        public ServerGame()
        {
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

            if (OnUpdateComplete != null)
            {
                OnUpdateComplete(this, null);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            return;
        }
    }
}
