﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            this.ComponentType = string.Empty;
            this.Name = string.Empty;
            this.SizeX = 0;
            this.SizeY = 0;
            this.PositionX = 0;
            this.PositionY = 0;
            this.Title = string.Empty;
            this.Text = string.Empty;
            this.Resizeable = false;
            this.Anchor = AnchorStyles.None;
            this.CursorType = Cursors.Default;
            this.Children = new List<UIComponent>();
            this.Enabled = true;
            this.Visible = true;
            this.Modal = false;
            this.Multiselect = false;
            this.MaxSelected = 1;
        }
    }
}
