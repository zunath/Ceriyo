using Ceriyo.Data.Enumerations;

namespace Ceriyo.Data.ResourceObjects
{
    public class CPResource
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
        public CPResourceType ResourceEditorType { get; set; }


        public CPResource()
        {
            FileName = string.Empty;
            Extension = string.Empty;
            Contents = string.Empty;
            SizeBytes = 0;
            ResourceEditorType = new CPResourceType();
        }
    }
}
