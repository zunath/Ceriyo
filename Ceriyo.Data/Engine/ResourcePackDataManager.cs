using System.Collections.Generic;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;
using FlatRedBall.IO;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Ceriyo.Data.Engine
{
    public static class ResourcePackDataManager
    {
        public static BindingList<string> GetAllResourcePackNames()
        {
            string[] files = Directory.GetFiles(EnginePaths.ResourcePacksDirectory, "*" + EnginePaths.ResourcePackExtension);
            BindingList<string> result = new BindingList<string>();

            foreach (string item in files)
            {
                result.Add(Path.GetFileName(item));
            }

            return result;
        }

        public static void BuildModule(BindingList<string> resourcePackFileNames)
        {
            BindingList<GameResource> gameResources = new BindingList<GameResource>();
            GameModule module = WorkingDataManager.GetGameModule();
            module.ResourcePacks = resourcePackFileNames;

            foreach (string package in resourcePackFileNames)
            {
                List<ResourceEditorItem> resources =
                    FileManager.XmlDeserialize<List<ResourceEditorItem>>(EnginePaths.ResourcePacksDirectory + package);

                foreach (ResourceEditorItem item in resources)
                {
                    GameResource gameResource = new GameResource
                    {
                        Package = package,
                        FileName = item.FileName,
                        ResourceType = item.ResourceType,
                        ResourceSubType = item.ResourceSubType,
                        Contents = item.Contents
                    };

                    if (gameResources.SingleOrDefault(x => x.FileName == item.FileName) == null &&
                        item.ResourceType != ResourceType.Unknown)
                    {
                        gameResources.Add(gameResource);
                    }
                }
            }

            WorkingDataManager.SaveModuleSettings(module);
            FileManager.XmlSerialize(gameResources, WorkingPaths.ResourceLinksFile);
        }

        public static BindingList<GameResource> GetGameResources(ResourceType resourceType, ResourceSubType resourceSubType)
        {
            string path = EnginePaths.WorkingDirectory + EnginePaths.ResourceLinksDataFileName + EnginePaths.DataExtension;
            BindingList<GameResource> resources = FileManager.XmlDeserialize<BindingList<GameResource>>(path);
            resources = new BindingList<GameResource>(resources.Where(x => x.ResourceType == resourceType && x.ResourceSubType == resourceSubType).ToList());
            
            return resources;
        }
    }
}
