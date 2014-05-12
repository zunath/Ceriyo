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

        public static FileOperationResultTypeEnum SaveModule(GameModule module)
        {
            FileOperationResultTypeEnum result = FileOperationResultTypeEnum.Unknown;

            try
            {
                FileManager.XmlSerialize(module, EnginePaths.WorkingDirectory + EnginePaths.ModuleDataFileName + EnginePaths.ModuleExtension);
                result = FileOperationResultTypeEnum.Success;
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

        public static GameModule GetGameModule()
        {
            string path = EnginePaths.WorkingDirectory + EnginePaths.ModuleDataFileName + EnginePaths.DataExtension;
            return FileManager.XmlDeserialize<GameModule>(path);
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

    }
}
