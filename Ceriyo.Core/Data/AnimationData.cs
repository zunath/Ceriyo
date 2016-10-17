using System;
using System.Collections.Generic;

namespace Ceriyo.Core.Data
{
    public class AnimationData
    {
        public string GlobalID { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public string Resref { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public IEnumerable<FrameData> Frames { get; set; }


        public AnimationData()
        {
            GlobalID = Guid.NewGuid().ToString();
            Frames = new List<FrameData>();
        }
    }
}
