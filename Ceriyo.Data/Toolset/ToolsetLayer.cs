
namespace Ceriyo.Data.Toolset
{
    public class ToolsetLayer
    {
        public int Layer { get; set; }
        public string Name { get { return "Layer " + Layer; } }

        public ToolsetLayer(int layerID)
        {
            Layer = layerID;
        }

        public ToolsetLayer()
        {
            Layer = 0;
        }
    }
}
