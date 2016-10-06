using Ceriyo.Core.Constants;

namespace Ceriyo.Core.Data
{
    public class ResourceItemData
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public ResourceType ResourceType { get; set; }
    }
}
