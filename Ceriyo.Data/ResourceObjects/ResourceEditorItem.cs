using Ceriyo.Data.Enumerations;

namespace Ceriyo.Data.ResourceObjects
{
    public class ResourceEditorItem
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public long SizeBytes { get; set; }
        public string Contents { get; set; }

        public ResourceType ResourceType
        {
            get { return ResourceEditorType.Type; }
        }

        public ResourceSubType ResourceSubType
        {
            get { return ResourceEditorType.SubType; }
        }
        public ResourceEditorType ResourceEditorType { get; set; }


        public ResourceEditorItem()
        {
            FileName = string.Empty;
            Extension = string.Empty;
            Contents = string.Empty;
            SizeBytes = 0;
            ResourceEditorType = new ResourceEditorType();
        }
    }
}
