using Ceriyo.Core.Attributes;

namespace Ceriyo.Core.Settings
{
    [FilePath("./Settings/Toolset.settings")]
    public class ToolsetSettings
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool IsMaximized { get; set; }
    }
}
