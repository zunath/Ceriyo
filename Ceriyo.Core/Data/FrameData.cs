using System;

namespace Ceriyo.Core.Data
{
    public class FrameData
    {
        public string GlobalID { get; set; }

        public string Name { get; set; }

        public bool FlipHorizontal { get; set; }

        public bool FlipVertical { get; set; }

        public float FrameLength { get; set; }

        public int TextureCellX { get; set; }

        public int TextureCellY { get; set; }

        public FrameData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
