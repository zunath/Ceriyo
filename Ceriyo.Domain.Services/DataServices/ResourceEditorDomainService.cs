using System.Collections.Generic;
using Ceriyo.Core.Data;
using Ceriyo.Domain.Services.DataServices.Contracts;

namespace Ceriyo.Domain.Services.DataServices
{
    public class ResourceEditorDomainService: IResourceEditorDomainService
    {
        public void SaveResourcePack(IEnumerable<ResourceItemData> resources, string resourcePackName)
        {

        }

        public IEnumerable<ResourceItemData> LoadResourcePack(string resourcePackName)
        {
            return null;
        }
    }
}
