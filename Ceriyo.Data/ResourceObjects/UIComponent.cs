using System.Collections.Generic;
using Squid;

namespace Ceriyo.Data.ResourceObjects
{
    public class UIComponent
    {
        public string ComponentType { get; set; }
        public string Name { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool Resizeable { get; set; }
        public AnchorStyles Anchor { get; set; }
        public string CursorType { get; set; }
        public List<UIComponent> Children { get; set; }
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        public bool Modal { get; set; }
        public bool Multiselect { get; set; }
        public int MaxSelected { get; set; }

        public UIComponent()
        {
            ComponentType = string.Empty;
            Name = string.Empty;
            SizeX = 0;
            SizeY = 0;
            PositionX = 0;
            PositionY = 0;
            Title = string.Empty;
            Text = string.Empty;
            Resizeable = false;
            Anchor = AnchorStyles.None;
            CursorType = Cursors.Default;
            Children = new List<UIComponent>();
            Enabled = true;
            Visible = true;
            Modal = false;
            Multiselect = false;
            MaxSelected = 1;
        }
    }
}
