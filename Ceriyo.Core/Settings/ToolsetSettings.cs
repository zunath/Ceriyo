using Ceriyo.Core.Attributes;

namespace Ceriyo.Core.Settings
{
    /// <summary>
    /// Tracks toolset settings which are adjusted by the end user.
    /// </summary>
    [FilePath("./Settings/Toolset.settings")]
    public class ToolsetSettings
    {
        /// <summary>
        /// Width of the toolset window.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of the toolset window.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// X position of the toolset window.
        /// </summary>
        public int PositionX { get; set; }

        /// <summary>
        /// Y position of the toolset window.
        /// </summary>
        public int PositionY { get; set; }

        /// <summary>
        /// Whether or not the toolset window is maximized.
        /// </summary>
        public bool IsMaximized { get; set; }
    }
}
