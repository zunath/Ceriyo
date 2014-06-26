using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.GameObjects
{
    public class SpriteAnimationFrame
    {
        private float _frameLength;

        public string Name { get; set; }
        public bool FlipHorizontal { get; set; }
        public bool FlipVertical { get; set; }
        public float FrameLength 
        {
            get
            {
                return _frameLength;
            }
            set
            {
                _frameLength = value;
                if (_frameLength < EngineConstants.MinimumAnimationFrameLength)
                {
                    _frameLength = EngineConstants.MinimumAnimationFrameLength;
                }
                else if (_frameLength > EngineConstants.MaximumAnimationFrameLength)
                {
                    _frameLength = EngineConstants.MaximumAnimationFrameLength;
                }
            }
        }
        public int TextureCellX { get; set; }
        public int TextureCellY { get; set; }

        public SpriteAnimationFrame()
        {
            this.Name = string.Empty;
            this.FlipHorizontal = false;
            this.FlipVertical = false;
            this.FrameLength = EngineConstants.MinimumAnimationFrameLength;
            this.TextureCellX = 0;
            this.TextureCellY = 0;
        }

        public SpriteAnimationFrame(string name)
        {
            this.Name = name;
            this.FlipHorizontal = false;
            this.FlipVertical = false;
            this.FrameLength = EngineConstants.MinimumAnimationFrameLength;
            this.TextureCellX = 0;
            this.TextureCellY = 0;
        }
    }
}
