using Ceriyo.Core.Constants;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class ResourceItemData : IDataDomainObject
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public ResourceType ResourceType { get; set; }
        public byte[] Data { get; set; }
    }
}
