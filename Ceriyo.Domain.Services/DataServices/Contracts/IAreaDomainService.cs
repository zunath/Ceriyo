using Ceriyo.Core.Data;

namespace Ceriyo.Domain.Services.DataServices.Contracts
{
    public interface IAreaDomainService
    {
        void SaveArea(AreaData data);
        void DeleteArea(AreaData data);
    }
}
