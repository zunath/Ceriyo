using System.Collections.Generic;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Data;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Core.Services.Contracts
{
    /// <summary>
    /// Service used for retrieving module resources such as graphics and sounds.
    /// </summary>
    public interface IModuleResourceService
    {
        /// <summary>
        /// Retrieves all resource names by a given resource type.
        /// </summary>
        /// <param name="resourceType">The type of resources to look for.</param>
        /// <returns>All resource names for a given resource type.</returns>
        IEnumerable<string> GetResourceNamesByType(ResourceType resourceType);

        /// <summary>
        /// Gets an individual resource by name and type.
        /// </summary>
        /// <param name="resourceType">The type of resource to look for.</param>
        /// <param name="resourceName">The name of the resource to look for.</param>
        /// <returns>The deserialized resource.</returns>
        ResourceItemData GetResourceByName(ResourceType resourceType, string resourceName);

        /// <summary>
        /// Loads a Texture2D directly from a graphic resource.
        /// </summary>
        /// <param name="resourceType">The type of resource to look for.</param>
        /// <param name="resourceName">The name of the resource to look for.</param>
        /// <returns>The Texture2D of the graphic resource.</returns>
        Texture2D LoadTexture2D(ResourceType resourceType, string resourceName);
    }
}
