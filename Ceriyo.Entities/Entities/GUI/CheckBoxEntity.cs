using Ceriyo.Data;
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
    public class CheckBoxEntity : BaseEntity, IClickable
    {
        public event EventHandler<EventArgs> OnChecked;
        public event EventHandler<EventArgs> OnUnchecked;

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
            }
        }
        private Text TextGraphic { get; set; }
        private Sprite EntitySprite { get; set; }
        private Texture2D UncheckedDefault { get; set; }
        private Texture2D UncheckedDown { get; set; }
        private Texture2D UncheckedHot { get; set; }
        private Texture2D CheckedDefault { get; set; }
        private Texture2D CheckedHot { get; set; }
        public bool IsChecked { get; private set; }

        public CheckBoxEntity(string text)
            : base("CheckBoxEntity")
        {
            this.Text = text;
        }

        protected override void CustomInitialize()
        {
            UncheckedDefault = FlatRedBallServices.Load<Texture2D>(EnginePaths.GUIDirectory + "checkbox_default.png");
            UncheckedHot = FlatRedBallServices.Load<Texture2D>(EnginePaths.GUIDirectory + "checkbox_hot.png");
            UncheckedDown = FlatRedBallServices.Load<Texture2D>(EnginePaths.GUIDirectory + "checkbox_down.png");
            CheckedDefault = FlatRedBallServices.Load<Texture2D>(EnginePaths.GUIDirectory + "checkbox_checked.png");
            CheckedHot = FlatRedBallServices.Load<Texture2D>(EnginePaths.GUIDirectory + "checkbox_checked_hot.png");

            IsChecked = false;
            EntitySprite = new Sprite();
            EntitySprite.PixelSize = 0.5f;
            EntitySprite.Alpha = 0.85f;
            EntitySprite.Texture = UncheckedDefault;
            TextGraphic = TextManager.AddText(Text);
            TextGraphic.AttachTo(EntitySprite, true);
            TextGraphic.HorizontalAlignment = HorizontalAlignment.Left;
            TextGraphic.RelativeX += 20;

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
            if (HasCursorOver(GuiManager.Cursor) && !InputManager.Mouse.ButtonDown(Mouse.MouseButtons.LeftButton))
            {
                if (IsChecked)
                {
                    EntitySprite.Texture = CheckedHot;
                }
                else
                {
                    EntitySprite.Texture = UncheckedHot;
                }
            }
            else
            {
                if (IsChecked)
                {
                    EntitySprite.Texture = CheckedDefault;
                }
                else
                {
                    EntitySprite.Texture = UncheckedDefault;
                }
            }
        }

        private void ButtonPress()
        {
            if (HasCursorOver(GuiManager.Cursor))
            {
                if (InputManager.Mouse.ButtonDown(Mouse.MouseButtons.LeftButton))
                {
                    if (!IsChecked)
                    {
                        EntitySprite.Texture = UncheckedDown;
                    }
                }

                else if (InputManager.Mouse.ButtonReleased(Mouse.MouseButtons.LeftButton))
                {
                    IsChecked = !IsChecked;

                    if (IsChecked)
                    {
                        EntitySprite.Texture = CheckedHot;

                        if (OnChecked != null)
                        {
                            OnChecked(this, new EventArgs());
                        }
                    }
                    else
                    {
                        EntitySprite.Texture = UncheckedHot;

                        if (OnUnchecked != null)
                        {
                            OnUnchecked(this, new EventArgs());
                        }
                    }

                }
            }
        }

        public bool HasCursorOver(Cursor cursor)
        {
            if (this.EntitySprite.Visible && (cursor.IsOn3D(EntitySprite) || cursor.IsOn3D(TextGraphic)))
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
