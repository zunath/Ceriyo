using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class SerializedFileData : IDataDomainObject
    {
        public string FilePath { get; set; }
        public byte[] Data { get; set; }
    }
}
