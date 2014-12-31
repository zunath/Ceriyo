using System.Collections.Generic;

namespace Ceriyo.Data.ResourceObjects
{
    public class UILayout
    {
        public List<UIComponent> Components { get; set; }

        public UILayout()
        {
            Components = new List<UIComponent>();
        }
    }
}
