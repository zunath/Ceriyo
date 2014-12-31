using System.Xml.Serialization;

namespace Ceriyo.Data.ResourceObjects
{
    public class ResourceEditorItem
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public long SizeBytes { get; set; }
        [XmlIgnore]
        public byte[] Contents { get; set; }

        public ResourceEditorItem()
        {
            FileName = string.Empty;
            Extension = string.Empty;
            SizeBytes = 0;
        }
    }
}
