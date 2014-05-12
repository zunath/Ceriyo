using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.ResourceObjects;
using FlatRedBall.IO;
using Ionic.Zip;

namespace Ceriyo.Data.Engine
{
    public static class ResourcePackDataManager
    {
        public static FileOperationResultTypeEnum SaveResourcePack(BindingList<ResourceEditorItem> resources, string path)
        {
            FileOperationResultTypeEnum result = FileOperationResultTypeEnum.Unknown;
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
                result = FileOperationResultTypeEnum.Success;
            }
            catch
            {
                result = FileOperationResultTypeEnum.Failure;
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
                            ResourceEditorItem item = new ResourceEditorItem();
                            item.FileName = Path.GetFileNameWithoutExtension(entry.FileName);
                            item.Extension = Path.GetExtension(entry.FileName);
                            item.SizeBytes = entry.UncompressedSize;

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

    }
}
