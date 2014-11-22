using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using FlatRedBall.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Ceriyo.Data
{
    public class WorkingDataManager
    {
        public FileOperationResultTypeEnum ReplaceAllGameObjectFiles(IEnumerable<IGameObject> gameObjects, string directory)
        {
            FileOperationResultTypeEnum result;
            string[] existingFiles = Directory.GetFiles(directory, "*" + EnginePaths.DataExtension);

            try
            {
                // Backup existing files.
                foreach (string file in existingFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string backupPath = directory + fileName + EnginePaths.BackupExtension;
                    File.Move(file, backupPath);
                }

                // Save new files
                foreach(IGameObject gameObject in gameObjects)
                {
                    string fileName = gameObject.Resref;
                    string path = directory + fileName + EnginePaths.DataExtension;
                    FileManager.XmlSerialize(gameObject.GetType(), gameObject, path);
                }

                // Remove backups
                string[] backupFiles = Directory.GetFiles(directory, "*" + EnginePaths.BackupExtension);
                foreach (string file in backupFiles)
                {
                    File.Delete(file);
                }

                result = FileOperationResultTypeEnum.Success;
            }
            catch
            {
                // Remove any partial new files
                string[] partialNewFiles = Directory.GetFiles(directory, "*" + EnginePaths.DataExtension);
                foreach(string partial in partialNewFiles)
                {
                    File.Delete(partial);
                }

                // Revert backups
                foreach (string file in existingFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string backupPath = directory + fileName + EnginePaths.BackupExtension;
                    if (File.Exists(backupPath))
                    {
                        File.Move(backupPath, file);
                    }
                }

                result = FileOperationResultTypeEnum.Failure;
            }

            return result;
        }


        public FileOperationResultTypeEnum SaveGameObjectFile(IGameObject gameObject)
        {
            FileOperationResultTypeEnum result;

            try
            {
                string filePath = gameObject.WorkingDirectory + gameObject.Resref + EnginePaths.DataExtension;
                FileManager.XmlSerialize(gameObject.GetType(), gameObject, filePath);

                result = FileOperationResultTypeEnum.Success;
            }
            catch
            {
                result = FileOperationResultTypeEnum.Failure;
            }

            return result;
        }

        public IGameObject OpenGameObjectFile(string relativeFilePath)
        {
            IGameObject result;

            try
            {
                string filePath = EnginePaths.WorkingDirectory + relativeFilePath;
                result = FileManager.XmlDeserialize(typeof(IGameObject), filePath) as IGameObject;
            }
            catch
            {
                result = null;
            }


            return result;
        }

        public FileOperationResultTypeEnum DeleteGameObjectFile(IGameObject gameObject)
        {
            FileOperationResultTypeEnum result;

            try
            {
                string filePath = gameObject.WorkingDirectory + gameObject.Resref + EnginePaths.DataExtension;

                if (!File.Exists(filePath))
                {
                    result = FileOperationResultTypeEnum.FileDoesNotExist;
                }
                else
                {
                    File.Delete(filePath);
                    result = FileOperationResultTypeEnum.Success;
                }
            }
            catch
            {
                result = FileOperationResultTypeEnum.Failure;
            }

            return result;
        }

        public BindingList<T> GetAllGameObjects<T>(string folderName) where T: IGameObject
        {
            BindingList<T> gameObjects = new BindingList<T>();

            string path = EnginePaths.WorkingDirectory + folderName + "/";
            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                T gameObject = FileManager.XmlDeserialize<T>(file);
                gameObjects.Add(gameObject);
            }

            return gameObjects;
        }

        public T GetGameObject<T>(string folderName, string resref) where T: IGameObject
        {
            try
            {
                string path = EnginePaths.WorkingDirectory + folderName + "/" + resref + EnginePaths.DataExtension;
                return FileManager.XmlDeserialize<T>(path);
            }
            catch
            {
                return default(T);
            }
        }

        public BindingList<string> GetAllScriptNames(bool insertBlankEntry = true)
        {
            BindingList<string> scripts = new BindingList<string>();

            try
            {
                string[] scriptNames = Directory.GetFiles(WorkingPaths.ScriptsDirectory, "*" + EnginePaths.ScriptExtension);
                foreach (string name in scriptNames)
                {
                    scripts.Add(Path.GetFileNameWithoutExtension(name));
                }

                if(insertBlankEntry)
                {
                    scripts.Insert(0, string.Empty);
                }
            }
            catch
            {
                scripts = null;
            }

            return scripts;
        }

        public FileOperationResultTypeEnum DeleteScript(string scriptName)
        {
            FileOperationResultTypeEnum result;

            try
            {
                string path = WorkingPaths.ScriptsDirectory + scriptName + EnginePaths.ScriptExtension;
                if (!File.Exists(path))
                {
                    result = FileOperationResultTypeEnum.FileDoesNotExist;
                }
                else
                {
                    File.Delete(path);
                    result = FileOperationResultTypeEnum.Success;
                }
            }
            catch
            {
                result = FileOperationResultTypeEnum.Failure;
            }

            return result;
        }

        public bool DoesGameObjectExist(IGameObject gameObject)
        {
            string path = gameObject.WorkingDirectory + gameObject.Resref + EnginePaths.DataExtension;

            return File.Exists(path);
        }

        public GameModule GetGameModule()
        {
            GameModule module;

            try
            {
                string path = EnginePaths.WorkingDirectory + EnginePaths.ModuleDataFileName + EnginePaths.DataExtension;
                module = FileManager.XmlDeserialize<GameModule>(path);

            }
            catch
            {
                module = null;
            }

            return module;
        }

        public FileOperationResultTypeEnum SaveModuleSettings(GameModule module)
        {
            FileOperationResultTypeEnum result;

            try
            {
                FileManager.XmlSerialize(module, EnginePaths.WorkingDirectory + EnginePaths.ModuleDataFileName + EnginePaths.DataExtension);
                result = FileOperationResultTypeEnum.Success;
            }
            catch
            {
                result = FileOperationResultTypeEnum.Failure;
            }

            return result;
        }
    }
}
