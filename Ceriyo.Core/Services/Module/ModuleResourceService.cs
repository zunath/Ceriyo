﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Services.Contracts;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Core.Services.Module
{
    /// <inheritdoc />
    public class ModuleResourceService: IModuleResourceService
    {
        private readonly IModuleService _moduleDomainService;
        private readonly IDataService _dataService;
        private readonly IPathService _pathService;
        private readonly GraphicsDevice _graphicsDevice;

        /// <inheritdoc />
        public ModuleResourceService(IModuleService moduleDomainService,
            IDataService dataService,
            IPathService pathService,
            GraphicsDevice graphicsDevice)
        {
            _moduleDomainService = moduleDomainService;
            _dataService = dataService;
            _pathService = pathService;
            _graphicsDevice = graphicsDevice;
        }
        

        private IEnumerable<string> GetResourcePacks()
        {
            return _moduleDomainService.GetLoadedModuleData().ResourcePacks;
        }

        private static string GetResourceTypePrefix(ResourceType resourceType)
        {
            return Enum.GetName(typeof(ResourceType), resourceType);
        }

        /// <inheritdoc />
        public IEnumerable<string> GetResourceNamesByType(ResourceType resourceType)
        {
            HashSet<string> resources = new HashSet<string>();
            string resourcePrefix = GetResourceTypePrefix(resourceType);
            if (string.IsNullOrWhiteSpace(resourcePrefix) || resourceType == ResourceType.Unknown)
            {
                throw new ArgumentException(nameof(resourceType));
            }

            IEnumerable<string> resourcePacks = GetResourcePacks();
            foreach (var resourcePack in resourcePacks)
            {
                SerializedManifestData manifest = _dataService.RetrieveManifest(_pathService.ResourcePackDirectory + resourcePack + ".rpk");
                var names = manifest.Where(x => x.Key.StartsWith(resourcePrefix)).Select(x => x.Key.Remove(0, resourcePrefix.Length));
                foreach (var name in names)
                {
                    if (!resources.Contains(name))
                    {
                        resources.Add(name);
                    }
                }
            }

            return resources;
        }

        /// <inheritdoc />
        public ResourceItemData GetResourceByName(ResourceType resourceType, string resourceName)
        {
            string fullResourceName = GetResourceTypePrefix(resourceType) + resourceName;
            IEnumerable<string> resourcePacks = GetResourcePacks();
            foreach (var pack in resourcePacks)
            {
                string path = _pathService.ResourcePackDirectory + pack + ".rpk";
                SerializedManifestData manifest = _dataService.RetrieveManifest(path);
                if (manifest.ContainsKey(fullResourceName))
                {
                    return _dataService.RetrieveSingleFile<ResourceItemData>(path, fullResourceName);
                }
            }

            return null;
        }

        /// <inheritdoc />
        public Texture2D LoadTexture2D(ResourceType resourceType, string resourceName)
        {
            ResourceType[] validTypes = { ResourceType.Creature, ResourceType.Icon, ResourceType.Item, ResourceType.Portrait, ResourceType.Tileset };
            if(!validTypes.Contains(resourceType))
                throw new ArgumentException($"{nameof(resourceType)} is of an invalid type. Must be one of the following: Creature, Icon, Item, Portrait, or Tileset.");

            if(string.IsNullOrWhiteSpace(resourceName))
                throw new ArgumentException($"{nameof(resourceName)} cannot be blank or null.");
            
            // Search for the resource in attached packages first.
            ResourceItemData data = GetResourceByName(resourceType, resourceName);
            
            // Couldn't find in attached resource packages. Look for an engine resource.
            if (data == null)
            {
                string path = _pathService.EngineGraphicsDirectory + GetResourceTypePrefix(resourceType) + "/" + resourceName;
                if (File.Exists(path))
                {
                    using (FileStream stream = File.OpenRead(path))
                    {
                        return Texture2D.FromStream(_graphicsDevice, stream);
                    }
                }
            }

            // Still couldn't find it. Throw exception.
            if (data == null)
            {
                throw new FileNotFoundException($"Couldn't find resource in attached resource packages or engine resources. Resource name: {resourceName}");
            }

            using (MemoryStream stream = new MemoryStream(data.Data))
            {
                return Texture2D.FromStream(_graphicsDevice, stream);
            }

        }

    }
}
