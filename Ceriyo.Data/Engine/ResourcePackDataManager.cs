using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;
using FlatRedBall.IO;
using Ionic.Zip;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Ceriyo.Data.Engine
{
    public static class ResourcePackDataManager
    {
        public static FileOperationResultType SaveResourcePack(BindingList<ResourceEditorItem> resources, string path)
        {
            FileOperationResultType result;
            string backupFilePath = path + EnginePaths.BackupExtension;
            
            try
            {
                if (File.Exists(path))
                {
                    File.Move(path, backupFilePath);
                }

                using (ZipFile file = new ZipFile(path))
                {
                    string xml;
                    FileManager.XmlSerialize(resources, out xml);

                    file.AddEntry(EnginePaths.ResourcePackDataFileName + EnginePaths.DataExtension, xml);

                    foreach (ResourceEditorItem item in resources)
                    {
                        file.AddEntry(item.FileName + item.Extension, item.Contents);
                    }

                    file.Save();
                }

                File.Delete(backupFilePath);
                result = FileOperationResultType.Success;
            }
            catch
            {
                result = FileOperationResultType.Failure;
                File.Delete(path);
                File.Move(backupFilePath, path);
            }

            return result;
        }

        public static BindingList<ResourceEditorItem> OpenResourcePack(string path)
        {
            BindingList<ResourceEditorItem> resources = new BindingList<ResourceEditorItem>();

            try
            {
                using (ZipFile file = new ZipFile(path))
                {
                    foreach (ZipEntry entry in file.Entries)
                    {
                        if (entry.FileName != EnginePaths.ResourcePackDataFileName + EnginePaths.DataExtension)
                        {
                            ResourceEditorItem item = new ResourceEditorItem
                            {
                                FileName = Path.GetFileNameWithoutExtension(entry.FileName),
                                Extension = Path.GetExtension(entry.FileName),
                                SizeBytes = entry.UncompressedSize
                            };

                            using (MemoryStream stream = new MemoryStream())
                            {
                                entry.Extract(stream);
                                item.Contents = stream.ToArray();
                            }

                            resources.Add(item);
                        }
                    }
                    
                }
            }
            catch
            {
                resources = null;
            }

            return resources;
        }

        public static BindingList<string> GetAllResourcePackNames()
        {
            BindingList<string> result = new BindingList<string>();

            try
            {
                string[] files = Directory.GetFiles(EnginePaths.ResourcePacksDirectory, "*" + EnginePaths.ResourcePackExtension);
                
                foreach (string file in files)
                {
                    result.Add(Path.GetFileName(file));
                }
            }
            catch
            {
                result = null;
            }

            return result;
        }

        public static bool BuildModule(BindingList<string> resourcePackFileNames)
        {
            bool success;
            BindingList<GameResource> resources = new BindingList<GameResource>();
            GameModule module = WorkingDataManager.GetGameModule();
            module.ResourcePacks = resourcePackFileNames;

            try
            {
                foreach (string package in resourcePackFileNames)
                {
                    using (ZipFile zip = new ZipFile(EnginePaths.ResourcePacksDirectory + package))
                    {
                        foreach (ZipEntry resourceFile in zip.Entries)
                        {
                            string extension = Path.GetExtension(resourceFile.FileName);
                            GameResource resource = new GameResource
                            {
                                Package = package,
                                FileName = resourceFile.FileName
                            };

                            switch (extension)
                            {
                                    // TODO: Force user to select type of resource
                                //case ".png":
                                //    resource.ResourceType = ResourceType.Graphic;
                                //    break;
                                //case ".mp3":
                                //    resource.ResourceType = ResourceType.Audio;
                                //    break;
                                default:
                                    resource.ResourceType = ResourceType.Unknown;
                                    break;
                            }

                            if (resources.SingleOrDefault(x => x.FileName == resource.FileName) == null && 
                                resource.ResourceType != ResourceType.Unknown)
                            {
                                resources.Add(resource);
                            }
                        }
                    }
                }

                WorkingDataManager.SaveModuleSettings(module);
                FileManager.XmlSerialize(resources, WorkingPaths.ResourceLinksFile);
                success = true;
            }
            catch
            {
                success = false;
            }

            return success;
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
