using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using FlatRedBall.IO;
using Ionic.Zip;

namespace Ceriyo.Data
{
    public static class ModuleDataManager
    {
        public static FileOperationResultTypeEnum CreateModule(string name, string tag, string resref)
        {
            FileOperationResultTypeEnum result = FileOperationResultTypeEnum.Unknown;

            try
            {
                if (!Directory.Exists(EnginePaths.ModulesDirectory))
                {
                    Directory.CreateDirectory(EnginePaths.ModulesDirectory);
                }
                
                string path = EnginePaths.ModulesDirectory + resref + EnginePaths.ModuleExtension;

                if (File.Exists(path))
                {
                    result = FileOperationResultTypeEnum.FileExists;
                }
                else
                {
                    GameModule module = new GameModule(name, tag, resref);

                    string levelPath = EnginePaths.DataDirectory + "LevelChart" + EnginePaths.DataExtension;
                    if (File.Exists(levelPath))
                    {
                        module.Levels = FileManager.XmlDeserialize<LevelChart>(levelPath);
                    }

                    using (ZipFile zip = new ZipFile(path))
                    {
                        AddDirectories(zip);
                        AddModulePropertiesFile(zip, module);
                        AddItemTypesFiles(zip);

                        zip.Save();
                    }

                    result = FileOperationResultTypeEnum.Success;
                }

            }
            catch
            {
                result = FileOperationResultTypeEnum.Failure;
            }

            return result;
        }


        public static FileOperationResultTypeEnum LoadModule(string resref, bool forceDeleteWorkingDirectory = false)
        {
            FileOperationResultTypeEnum result = FileOperationResultTypeEnum.Unknown;

            try
            {
                if (!Directory.Exists(EnginePaths.WorkingDirectory))
                {
                    Directory.CreateDirectory(EnginePaths.WorkingDirectory);
                }

                string path = EnginePaths.ModulesDirectory + resref + EnginePaths.ModuleExtension;
                if (!File.Exists(path))
                {
                    result = FileOperationResultTypeEnum.FileDoesNotExist;
                }
                else
                {
                    if (Directory.GetFiles(EnginePaths.WorkingDirectory).Length > 0)
                    {
                        if (forceDeleteWorkingDirectory)
                        {
                            Directory.Delete(EnginePaths.WorkingDirectory, true);
                            Directory.CreateDirectory(EnginePaths.WorkingDirectory);
                        }
                        else
                        {
                            result = FileOperationResultTypeEnum.FileExists;
                        }
                    }

                    if (result != FileOperationResultTypeEnum.FileExists)
                    {
                        using (ZipFile zip = new ZipFile(path))
                        {
                            AddDirectories(zip);
                            zip.Save();

                            zip.ExtractAll(EnginePaths.WorkingDirectory);
                        }

                        result = FileOperationResultTypeEnum.Success;
                    }
                }
            }
            catch
            {
                result = FileOperationResultTypeEnum.Failure;
            }

            return result;
        }

        private static void AddDirectories(ZipFile zip)
        {
            if (zip[ModulePaths.CharacterClassesDirectory] == null)
            {
                zip.AddDirectoryByName(ModulePaths.CharacterClassesDirectory);
            }

            if (zip[ModulePaths.DialogsDirectory] == null)
            {
                zip.AddDirectoryByName(ModulePaths.DialogsDirectory);
            }

            if (zip[ModulePaths.CreaturesDirectory] == null)
            {
                zip.AddDirectoryByName(ModulePaths.CreaturesDirectory);
            }
            if (zip[ModulePaths.ItemsDirectory] == null)
            {
                zip.AddDirectoryByName(ModulePaths.ItemsDirectory);
            }
            if (zip[ModulePaths.ItemTypesDirectory] == null)
            {
                zip.AddDirectoryByName(ModulePaths.ItemTypesDirectory);
            }
            if (zip[ModulePaths.ItemPropertiesDirectory] == null)
            {
                zip.AddDirectoryByName(ModulePaths.ItemPropertiesDirectory);
            }
            if (zip[ModulePaths.AreasDirectory] == null)
            {
                zip.AddDirectoryByName(ModulePaths.AreasDirectory);
            }
            if (zip[ModulePaths.PlaceablesDirectory] == null)
            {
                zip.AddDirectoryByName(ModulePaths.PlaceablesDirectory);
            }
            if (zip[ModulePaths.ScriptsDirectory] == null)
            {
                zip.AddDirectoryByName(ModulePaths.ScriptsDirectory);
            }
            if (zip[ModulePaths.TilesetsDirectory] == null)
            {
                zip.AddDirectoryByName(ModulePaths.TilesetsDirectory);
            }

        }

        private static void AddModulePropertiesFile(ZipFile zip, GameModule module)
        {
            string output = "";
            FileManager.XmlSerialize<GameModule>(module, out output);

            zip.AddEntry(EnginePaths.ModuleDataFileName + EnginePaths.DataExtension, output);
        }

        private static void AddItemTypesFiles(ZipFile zip)
        {
            string itemTypesPath = EnginePaths.DataDirectory + ModulePaths.ItemTypesDirectory;
            if (Directory.Exists(itemTypesPath))
            {
                foreach (string file in Directory.GetFiles(itemTypesPath))
                {
                    try
                    {
                        ItemType itemType = FileManager.XmlDeserialize<ItemType>(file);
                        // Serialization worked - copy the file to the module zip
                        zip.AddFile(Path.GetFileName(file), ModulePaths.ItemTypesDirectory);
                    }
                    catch
                    {
                        // TODO: Log entry maybe?
                    }
                }
            }
        }

        private static GameModule GetGameModule(string zipFilePath)
        {
            using (ZipFile zip = new ZipFile(zipFilePath))
            {
                ZipEntry entry = zip[EnginePaths.ModuleDataFileName + EnginePaths.DataExtension];
                MemoryStream stream = new MemoryStream();
                entry.Extract(stream);
                string text = Encoding.ASCII.GetString(stream.ToArray());
                return FileManager.XmlDeserializeFromString<GameModule>(text);
            }
        }

        public static IList<GameModule> GetModules()
        {
            List<GameModule> modules = new List<GameModule>();

            string[] filePaths = Directory.GetFiles(EnginePaths.ModulesDirectory);
            foreach (string file in filePaths)
            {
                GameModule deserialized = GetGameModule(file);
                modules.Add(deserialized);
            }

            return modules;
        }

        public static FileOperationResultTypeEnum SaveModule(string moduleResref)
        {
            FileOperationResultTypeEnum result = FileOperationResultTypeEnum.Unknown;
            string path = EnginePaths.ModulesDirectory + moduleResref + EnginePaths.ModuleExtension;
            string backup = EnginePaths.ModulesDirectory + moduleResref + EnginePaths.ModuleExtension + EnginePaths.BackupExtension;
                
            try
            {
                if (File.Exists(path))
                {
                    File.Move(path, backup);
                }

                using (ZipFile zip = new ZipFile(path))
                {
                    zip.AddDirectory(WorkingPaths.DataDirectory, "Data");
                    zip.AddFile(EnginePaths.WorkingDirectory + EnginePaths.ModuleDataFileName + EnginePaths.DataExtension, "");
                    zip.AddFile(EnginePaths.WorkingDirectory + EnginePaths.ResourceLinksDataFileName + EnginePaths.DataExtension, "");

                    zip.Save();
                }

                if (File.Exists(backup))
                {
                    File.Delete(backup);
                }

                result = FileOperationResultTypeEnum.Success;
            }
            catch
            {
                if (File.Exists(backup))
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    File.Move(backup, path);
                }

                result = FileOperationResultTypeEnum.Failure;
            }

            return result;
        }

    }
}
