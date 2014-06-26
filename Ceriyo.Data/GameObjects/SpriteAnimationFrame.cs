using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.GameObjects
{
    public class SpriteAnimationFrame
    {
        public string Name { get; set; }
        public bool FlipHorizontal { get; set; }
        public bool FlipVertical { get; set; }
        public float FrameLength { get; set; }
        public int TextureCellX { get; set; }
        public int TextureCellY { get; set; }

        public SpriteAnimationFrame()
        {
            this.Name = string.Empty;
            this.FlipHorizontal = false;
            this.FlipVertical = false;
            this.FrameLength = 0.0f;
            this.TextureCellX = 0;
            this.TextureCellY = 0;
        }

        public SpriteAnimationFrame(string name)
        {
            this.Name = name;
            this.FlipHorizontal = false;
            this.FlipVertical = false;
            this.FrameLength = 0.0f;
            this.TextureCellX = 0;
            this.TextureCellY = 0;
        }
    }
}
