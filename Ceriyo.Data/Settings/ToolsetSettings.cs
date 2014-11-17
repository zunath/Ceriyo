
namespace Ceriyo.Data.Settings
{
    public class ToolsetSettings
    {
        public int DataEditorHeight { get; set; }
        public int DataEditorWidth { get; set; }

        public int MainWindowHeight { get; set; }
        public int MainWindowWidth { get; set; }


        public ToolsetSettings()
        {
            DataEditorHeight = 600;
            DataEditorWidth = 700;

            MainWindowHeight = 600;
            MainWindowWidth = 800;
        }
    }
}
