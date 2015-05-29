using Ceriyo.Data.Enumerations;

namespace Ceriyo.Data.ResourceObjects
{
    public class ResourceEditorType
    {
        public string Name { get; set; }
        public ResourceType Type { get; set; }
        public ResourceSubType SubType { get; set; }

        public ResourceEditorType()
        {
            Name = string.Empty;
            Type = ResourceType.None;
            SubType = ResourceSubType.None;
        }

        public ResourceEditorType(string name, ResourceType resourceType, ResourceSubType resourceSubType)
        {
            Name = name;
            Type = resourceType;
            SubType = resourceSubType;
        }

    }
}
