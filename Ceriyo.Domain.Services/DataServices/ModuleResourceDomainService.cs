using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Domain.Services.DataServices.Contracts;

namespace Ceriyo.Domain.Services.DataServices
{
    public class ModuleResourceDomainService: IModuleResourceDomainService
    {
        private readonly IModuleDomainService _moduleDomainService;
        private readonly IDataService _dataService;
        private readonly IPathService _pathService;

        public ModuleResourceDomainService(IModuleDomainService moduleDomainService,
            IDataService dataService,
            IPathService pathService)
        {
            _moduleDomainService = moduleDomainService;
            _dataService = dataService;
            _pathService = pathService;
        }

        private string[] GetResourcePacks()
        {
            return _moduleDomainService.GetLoadedModuleData().ResourcePacks.ToArray();
        }

        private string GetResourceTypePrefix(ResourceType resourceType)
        {
            return Enum.GetName(typeof(ResourceType), resourceType);
        }

        public IEnumerable<string> GetResourceNamesByType(ResourceType resourceType)
        {
            HashSet<string> resources = new HashSet<string>();
            string resourcePrefix = GetResourceTypePrefix(resourceType);
            if (string.IsNullOrWhiteSpace(resourcePrefix) || resourceType == ResourceType.Unknown)
            {
                throw new ArgumentException(nameof(resourceType));
            }

            string[] resourcePacks = GetResourcePacks();
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

        public ResourceItemData GetResourceByName(ResourceType resourceType, string resourceName)
        {
            string fullResourceName = GetResourceTypePrefix(resourceType) + resourceName;
            string[] resourcePacks = GetResourcePacks();
            foreach (var pack in resourcePacks)
            {
                string path = _pathService.ResourcePackDirectory + pack + ".rpk";
                SerializedManifestData manifest = _dataService.RetrieveManifest(path);
                if (manifest.ContainsKey(fullResourceName))
                {
                    return _dataService.RetrieveSingleFile<ResourceItemData>(path, fullResourceName);
                }
            }

            throw new FileNotFoundException($"Resource could not be found in any attached resource packs. Name: {resourceName}");
        }
    }
}
