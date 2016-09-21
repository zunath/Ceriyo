using System;

namespace Ceriyo.Core.Data
{
    public class TilesetData
    {
        public string GlobalID { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }

        public TilesetData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
