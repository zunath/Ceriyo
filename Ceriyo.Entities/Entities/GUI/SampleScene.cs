using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;
using SampleControls;
using Squid;

namespace Ceriyo.Library.SquidGUI
{
    public class SampleScene : DrawableGameComponent
    {
        private Desktop Desktop;


        public SampleScene()
            : base(FlatRedBallServices.Game)
        {
            this.UpdateOrder = 10;
        }

        protected override void LoadContent()
        {
            Desktop = new GameGui { Name = "desk" };
            Desktop = new SampleDesktop { Name = "desk" };
            Desktop.ShowCursor = true;


            base.LoadContent();
        }

        public override void Draw(GameTime time)
        {
            Desktop.Size = new Squid.Point(Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height);
            Desktop.Update();
            Desktop.Draw();

            int tex = GuiHost.Renderer.GetTexture("xna_logo.png");
            Squid.Point size = GuiHost.Renderer.GetTextureSize(tex);
            Squid.Rectangle rect = new Squid.Rectangle(0, 0, size.x, size.y);

            GuiHost.Renderer.StartBatch();
            GuiHost.Renderer.DrawTexture(tex, Game.GraphicsDevice.Viewport.Width - 130, Game.GraphicsDevice.Viewport.Height - 130, 128, 128, rect, -1);
            GuiHost.Renderer.EndBatch(true);
        }

        private void ReadAtlas(string mapName)
        {
            Atlas atlas = new Atlas();
            atlas.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "Content\\" + mapName + ".xml");

            foreach (ControlStyle style in GuiHost.GetSkin().Styles.Values)
            {
                foreach (Style state in style.Styles.Values)
                {
                    if (string.IsNullOrEmpty(state.Texture))
                        continue;

                    if (atlas.Contains(state.Texture))
                    {
                        state.TextureRect = atlas.GetRect(state.Texture);
                        state.Texture = mapName + ".png";
                    }
                }
            }
        }
    }
}
