using System.Collections.Generic;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Data;

namespace Ceriyo.Domain.Services.DataServices.Contracts
{
    public interface IModuleResourceDomainService
    {
        IEnumerable<string> GetResourceNamesByType(ResourceType resourceType);
        ResourceItemData GetResourceByName(ResourceType resourceType, string resourceName);
    }
}
