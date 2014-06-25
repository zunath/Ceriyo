using Ceriyo.Data;
using Ceriyo.Data.ResourceObjects;
using Ceriyo.Library.Processing;
using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.Gui;
using FlatRedBall.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Entities.Entities.GUI
{
    public class ButtonEntity : BaseEntity, IClickable
    {
        public event EventHandler<EventArgs> OnClicked;
        public event EventHandler<EventArgs> OnHovered;

        private string _text;
        public string Text 
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                TextGraphic.DisplayText = value;
            }
        }
        private Text TextGraphic { get; set; }
        private Sprite EntitySprite { get; set; }
        private Texture2D DefaultTexture { get; set; }
        private Texture2D ButtonDownTexture { get; set; }
        private Texture2D ButtonHotTexture { get; set; }

        public ButtonEntity(string text)
            : base("ButtonEntity")
        {
            this.Text = text;
        }

        protected override void CustomInitialize()
        {
            DefaultTexture = FlatRedBallServices.Load<Texture2D>(EnginePaths.GUIDirectory + "button_default.png");
            ButtonDownTexture = FlatRedBallServices.Load<Texture2D>(EnginePaths.GUIDirectory + "button_down.png");
            ButtonHotTexture = FlatRedBallServices.Load<Texture2D>(EnginePaths.GUIDirectory + "button_hot.png");

            
            EntitySprite = new Sprite();
            EntitySprite.PixelSize = 0.5f;
            EntitySprite.Alpha = 0.85f;
            TextGraphic = TextManager.AddText(Text);
            TextGraphic.AttachTo(EntitySprite, true);
            TextGraphic.HorizontalAlignment = HorizontalAlignment.Center;

            EntitySprite.Texture = DefaultTexture;
            EntitySprite.AttachTo(this, false);

            SpriteManager.AddSprite(EntitySprite);
            
        }

        protected override void CustomActivity()
        {
            Highlight();
            ButtonPress();
        }

        protected override void CustomDestroy()
        {
            SpriteManager.RemoveSprite(EntitySprite);   
        }

        private void Highlight()
        {
            if (HasCursorOver(GuiManager.Cursor))
            {
                EntitySprite.Texture = ButtonHotTexture;

                if (OnHovered != null)
                {
                    OnHovered(this, new EventArgs());
                }
            }
            else
            {
                EntitySprite.Texture = DefaultTexture;
            }
        }

        private void ButtonPress()
        {
            if (InputManager.Mouse.ButtonDown(Mouse.MouseButtons.LeftButton))
            {
                EntitySprite.Texture = ButtonDownTexture;

                if (OnClicked != null)
                {
                    OnClicked(this, new EventArgs());
                }
            }
            else if (InputManager.Mouse.ButtonReleased(Mouse.MouseButtons.LeftButton))
            {
                EntitySprite.Texture = ButtonHotTexture;
            }
        }

        public bool HasCursorOver(Cursor cursor)
        {
            if (this.EntitySprite.Visible && cursor.IsOn3D(EntitySprite))
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
