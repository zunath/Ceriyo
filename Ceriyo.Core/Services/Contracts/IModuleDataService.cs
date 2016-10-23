using System.Collections.Generic;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Services.Contracts
{
    public interface IModuleDataService
    {
        T Load<T>(string globalID)
            where T : class, IDataDomainObject;

        IEnumerable<T> LoadAll<T>()
            where T : class, IDataDomainObject;
    }
}
