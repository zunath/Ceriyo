using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data
{
    public static class EngineConstants
    {
        public static string ApplicationIdentifier
        {
            get { return "D7434CF5-4108-4654-9C52-AA5217D4104D"; }
        }

        public static bool IsDebugEnabled
        {
            get { return true; } 
        }

        public static int TilePixelWidth
        {
            get { return 32; }
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

        public static int AreaMinWidth
        {
            get { return 2; }
        }

        public static int AreaMaxHeight
        {
            get { return 32; }
        }

        public static int AreaMinHeight
        {
            get { return 2; }
        }

        public static int AreaMaxLayers
        {
            get { return 4; }
        }

        public static int MaxLevel
        {
            get { return 99; }
        }

        public static int AnimationFrameWidth
        {
            get { return 70; }
        }

        public static int AnimationFrameHeight
        {
            get { return 70; }
        }

        public static float MinimumAnimationFrameLength
        {
            get { return 0.1f; }
        }

        public static float MaximumAnimationFrameLength
        {
            get { return 5.0f; }
        }

    }
}
