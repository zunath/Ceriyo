using System.Xml.Serialization;
using Ceriyo.Data.Enumerations;

namespace Ceriyo.Data.ResourceObjects
{
    public class ResourceEditorItem
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public long SizeBytes { get; set; }
        [XmlIgnore]
        public byte[] Contents { get; set; }
        public ResourceType ResourceType { get; set; }
        public ResourceSubType ResourceSubType { get; set; }

        public ResourceEditorItem()
        {
            FileName = string.Empty;
            Extension = string.Empty;
            SizeBytes = 0;
            ResourceType = ResourceType.None;
            ResourceSubType = ResourceSubType.None;
        }
    }
}
