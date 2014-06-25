using Ceriyo.Data;
using Ceriyo.Data.ResourceObjects;
using Ceriyo.Library.Processing;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Entities.Entities.GUI
{
    public class ButtonEntity : BaseEntity
    {
        public event EventHandler<EventArgs> OnButtonClicked;
        public string Text { get; private set; }
        private Text TextGraphic { get; set; }
        private Sprite EntitySprite { get; set; }
        private string DefaultImage = EnginePaths.GUIDirectory + "button_default.png";
        private string ButtonDownImage = EnginePaths.GUIDirectory + "button_down.png";
        private string ButtonHotImage = EnginePaths.GUIDirectory + "button_hot.png";

        public ButtonEntity(string text)
            : base("ButtonEntity")
        {
            this.Text = text;
        }

        protected override void CustomInitialize()
        {
            EntitySprite = new Sprite();
            EntitySprite.PixelSize = 0.5f;
            EntitySprite.Visible = true;
            TextGraphic = TextManager.AddText(Text);
            TextGraphic.AttachTo(EntitySprite, false);
            EntitySprite.Texture = FlatRedBallServices.Load<Texture2D>(DefaultImage);
            EntitySprite.AttachTo(this, false);

            SpriteManager.AddSprite(EntitySprite);
        }

        protected override void CustomActivity()
        {
        }

        protected override void CustomDestroy()
        {
            SpriteManager.RemoveSprite(EntitySprite);   
        }

        public void ChangeText(string newText)
        {
            this.Text = newText;
            TextGraphic.DisplayText = newText;
        }
    }
}
