using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.Enumerations;

namespace Ceriyo.Data.ResourceObjects
{
    public class GameResource
    {
        public string Package { get; set; }
        public string FileName { get; set; }
        public ResourceTypeEnum ResourceType { get; set; }

        public GameResource()
        {
            this.FileName = "";
            this.Package = "";
            this.ResourceType = ResourceTypeEnum.Unknown;
        }

        public GameResource(string package, string fileName, ResourceTypeEnum resourceType)
        {
            this.FileName = fileName;
            this.Package = package;
            this.ResourceType = resourceType;
        }
    }
}
