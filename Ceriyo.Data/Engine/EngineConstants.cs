using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data
{
    public static class EngineConstants
    {
        public static int TilePixelWidth
        {
            get { return 64; }
        }

        public static int TilePixelHeight
        {
            get { return 32; }
        }

        public static int NameMaxLength
        {
            get { return 32; }
        }

        public static int TagMaxLength
        {
            get { return 32; }
        }

        public static int ResrefMaxLength
        {
            get { return 32; }
        }

        public static int DescriptionMaxLength
        {
            get { return 2048; }
        }

        public static int CommentsMaxLength
        {
            get { return 2048; }
        }

        public static int AreaMaxWidth
        {
            get { return 32; }
        }

        public static int AreaMaxHeight
        {
            get { return 32; }
        }

        public static int AreaMaxLayers
        {
            get { return 4; }
        }

        public static int MaxLevel
        {
            get { return 99; }
        }

    }
}
