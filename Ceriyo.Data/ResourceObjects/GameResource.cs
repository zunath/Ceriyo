using System.IO;
using Ceriyo.Data.Enumerations;

namespace Ceriyo.Data.ResourceObjects
{
    public class GameResource
    {
        public string Name { get { return Path.GetFileNameWithoutExtension(FileName); } }
        public string Package { get; set; }
        public string FileName { get; set; }
        public ResourceType ResourceType { get; set; }

        public GameResource()
        {
            FileName = string.Empty;
            Package = string.Empty;
            ResourceType = ResourceType.Unknown;
        }

        public GameResource(string package, string fileName, ResourceType resourceType)
        {
            FileName = fileName;
            Package = package;
            ResourceType = resourceType;
        }
    }
}
