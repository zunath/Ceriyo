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
                
                string path = EnginePaths.ModulesDirectory + resref + ModulePaths.ModuleExtension;

                if (File.Exists(path))
                {
                    result = FileOperationResultTypeEnum.FileExists;
                }
                else
                {
                    GameModule module = new GameModule();

                    using (ZipFile zip = new ZipFile(path))
                    {
                        AddDirectories(zip);

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
                string path = EnginePaths.ModulesDirectory + resref + ModulePaths.ModuleExtension;
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

                            zip.ExtractAll(EnginePaths.WorkingDirectory);
                        }
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

        private static void AddDirectories(ZipFile zip)
        {
            zip.AddDirectoryByName(ModulePaths.CharacterClassesDirectory);
            zip.AddDirectoryByName(ModulePaths.ConversationsDirectory);
            zip.AddDirectoryByName(ModulePaths.CreaturesDirectory);
            zip.AddDirectoryByName(ModulePaths.ItemsDirectory);
            zip.AddDirectoryByName(ModulePaths.MapsDirectory);
            zip.AddDirectoryByName(ModulePaths.PlaceablesDirectory);
            zip.AddDirectoryByName(ModulePaths.ScriptsDirectory);
            zip.AddDirectoryByName(ModulePaths.TilesetsDirectory);

        }

    }
}
