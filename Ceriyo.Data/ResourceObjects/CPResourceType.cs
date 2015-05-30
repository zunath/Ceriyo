using Ceriyo.Data.Enumerations;

namespace Ceriyo.Data.ResourceObjects
{
    public class CPResourceType
    {
        public string Name { get; set; }
        public ResourceType Type { get; set; }
        public ResourceSubType SubType { get; set; }

        public CPResourceType()
        {
            Name = string.Empty;
            Type = ResourceType.None;
            SubType = ResourceSubType.None;
        }

        public CPResourceType(string name, ResourceType resourceType, ResourceSubType resourceSubType)
        {
            Name = name;
            Type = resourceType;
            SubType = resourceSubType;
        }

    }
}
