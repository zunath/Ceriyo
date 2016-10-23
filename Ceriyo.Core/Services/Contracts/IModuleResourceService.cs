using System.Collections.Generic;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Data;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Core.Services.Contracts
{
    public interface IModuleResourceService
    {
        IEnumerable<string> GetResourceNamesByType(ResourceType resourceType);
        ResourceItemData GetResourceByName(ResourceType resourceType, string resourceName);
        Texture2D LoadTexture2D(ResourceType resourceType, string resourceName);
    }
}
