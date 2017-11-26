using Ceriyo.Core.Constants;

namespace Ceriyo.Core.Data
{
    public class ResourceItemData
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public ResourceType ResourceType { get; set; }
        public byte[] Data { get; set; }
    }
}
