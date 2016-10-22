using System.Collections.Generic;
using Ceriyo.Core.Data;

namespace Ceriyo.Domain.Services.DataServices.Contracts
{
    public interface IResourceEditorDomainService
    {
        void SaveResourcePack(IEnumerable<ResourceItemData> resources, string resourcePackName);

        IEnumerable<ResourceItemData> LoadResourcePack(string resourcePackName);

        
    }
}
