using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Domain.Services.DataServices.Contracts
{
    public interface IDataDomainService
    {
        void SaveData<T>(T data)
            where T: IDataDomainObject;
        void DeleteData<T>(T data)
            where T: IDataDomainObject;
    }
}
