using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class TileData: IDataDomainObject
    {
        public int SourceX { get; set; }
        public int SourceY { get; set; }
    }
}
