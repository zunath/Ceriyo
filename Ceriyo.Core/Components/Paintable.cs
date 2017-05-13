using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    public class Paintable: IComponent
    {
        /// <summary>
        /// Height of the area
        /// </summary>
        public int AreaHeight { get; set; }
        /// <summary>
        /// Width of the area
        /// </summary>
        public int AreaWidth { get; set; }
    }
}
