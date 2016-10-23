using System;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class AreaData : IDataDomainObject
    {
        public string GlobalID { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public string Resref { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public string OnAreaEnter { get; set; }

        public string OnAreaExit { get; set; }

        public string OnAreaHeartbeat { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public LocalVariableData LocalVariables { get; set; }
        public string TilesetGlobalID { get; set; }


        public AreaData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalVariables = new LocalVariableData();
        }
    }
}
