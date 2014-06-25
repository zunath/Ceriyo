using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.Gui;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Entities.Entities.GUI
{
    public class WindowEntity : BaseEntity, IClickable
    {
        private Sprite WindowSprite { get; set; }

        private Texture2D WindowTexture { get; set; }
        private int Width { get; set; }
        private int Height { get; set; }

        public WindowEntity(int width, int height, string windowTitle = "")
            : base("WindowEntity")
        {
            WindowSprite = new Sprite();
            this.Width = width;
            this.Height = height;
        }

        protected override void CustomInitialize()
        {
            WindowSprite.PixelSize = 0.5f;
            WindowSprite.Texture = FlatRedBallServices.Load<Texture2D>(EnginePaths.GUIDirectory + "window.png");
            
            WindowSprite.Height = Height;
            WindowSprite.Width = Width;


            SpriteManager.AddSprite(WindowSprite);
            
        }

        protected override void CustomActivity()
        {
        }

        protected override void CustomDestroy()
        {
            SpriteManager.RemoveSprite(WindowSprite);
        }

        public bool HasCursorOver(Cursor cursor)
        {
            if(WindowSprite.Visible && cursor.IsOn3D(WindowSprite))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
