using System.Collections.Generic;
using Ceriyo.Core.Data;

namespace Ceriyo.Domain.Services.DataServices.Contracts
{
    /// <summary>
    /// Service used for saving and loading resource packages.
    /// </summary>
    public interface IResourceEditorDomainService
    {
        /// <summary>
        /// Saves a resource package.
        /// </summary>
        /// <param name="resources">The resources to save into the package.</param>
        /// <param name="resourcePackName">The name of the resource package.</param>
        void SaveResourcePack(IEnumerable<ResourceItemData> resources, string resourcePackName);

        /// <summary>
        /// Loads a resource package.
        /// </summary>
        /// <param name="resourcePackName">The package to load into memory.</param>
        /// <returns>An IEnumerable of ResourceItemData objects found in the resource package.</returns>
        IEnumerable<ResourceItemData> LoadResourcePack(string resourcePackName);

        
    }
}
