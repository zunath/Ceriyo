using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Domain.Services.DataServices.Contracts;

namespace Ceriyo.Domain.Services.DataServices
{
    public class ResourceEditorDomainService : IResourceEditorDomainService
    {
        private readonly IDataService _dataService;
        private const string BaseDirectory = "./ResourcePacks/";

        public ResourceEditorDomainService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void SaveResourcePack(IEnumerable<ResourceItemData> resources, string resourcePackName)
        {
            var resourceList = resources.ToList();
            string backupFilePath = resourcePackName + ".rpbk";
            resourcePackName = resourcePackName + ".rpk";
            if (File.Exists(BaseDirectory + resourcePackName))
            {
                File.Move(BaseDirectory + resourcePackName, BaseDirectory + backupFilePath);
            }
            else
            {
                backupFilePath = null;
            }
            try
            {
                using (var stream = File.Create(BaseDirectory + resourcePackName))
                {
                    SerializedManifestData manifest = new SerializedManifestData();
                    for (int index = 0; index < resourceList.Count(); index++)
                    {
                        var resource = resourceList[index];
                        manifest.Add(GetKeyOfResourceItem(resource), index);
                    }
                    _dataService.PackageFile(manifest, stream);

                    foreach (var resource in resourceList)
                    {
                        // Load data - either from the file system or from the existing rpk file.
                        if (string.IsNullOrWhiteSpace(resource.FilePath))
                        {
                            var backupEntry = _dataService.RetrieveSingleFile<ResourceItemData>(BaseDirectory + backupFilePath, GetKeyOfResourceItem(resource));
                            resource.Data = backupEntry.Data;
                        }
                        else
                        {
                            resource.Data = File.ReadAllBytes(resource.FilePath);
                        }

                        resource.FilePath = null;
                        _dataService.PackageFile(resource, stream);
                        resource.Data = null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Restore backup, if any.
                if (backupFilePath != null)
                {
                    if (File.Exists(BaseDirectory + resourcePackName))
                    {
                        File.Delete(BaseDirectory + resourcePackName);
                    }

                    File.Move(BaseDirectory + backupFilePath, BaseDirectory + resourcePackName);
                }
                
                throw new Exception("Unable to save resource pack.", ex);
            }
            

            if (File.Exists(BaseDirectory + backupFilePath))
            {
                File.Delete(BaseDirectory + backupFilePath);
            }

        }

        private static string GetKeyOfResourceItem(ResourceItemData resource)
        {
            return Enum.GetName(typeof(ResourceType), resource.ResourceType) + "/" + resource.FileName + resource.Extension;
        }

        public IEnumerable<ResourceItemData> LoadResourcePack(string resourcePackName)
        {
            string filePath = BaseDirectory + resourcePackName + ".rpk";
            var manifest = _dataService.RetrieveManifest(filePath);
            List<ResourceItemData> resources = new List<ResourceItemData>();

            foreach (var item in manifest)
            {
                var resource = _dataService.RetrieveSingleFile<ResourceItemData>(filePath, item.Key);
                resource.Data = null;

                resources.Add(resource);
            }

            return resources;
        }
    }
}
