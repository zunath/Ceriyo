using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using FlatRedBall.IO;

namespace Ceriyo.Data
{
    public static class WorkingDataManager
    {
        public static FileOperationResultTypeEnum SaveGameObjectFile(IGameObject gameObject)
        {
            FileOperationResultTypeEnum result = FileOperationResultTypeEnum.Unknown;

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

        public static IGameObject OpenGameObjectFile(string relativeFilePath)
        {
            IGameObject result = null;

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

        public static FileOperationResultTypeEnum DeleteGameObjectFile(IGameObject gameObject)
        {
            FileOperationResultTypeEnum result = FileOperationResultTypeEnum.Unknown;

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

        public static BindingList<T> GetAllGameObjects<T>(string folderName) where T: IGameObject
        {
            BindingList<T> gameObjects = new BindingList<T>();

            try
            {
                string path = EnginePaths.WorkingDirectory + folderName + "/";
                string[] files = Directory.GetFiles(path);

                foreach (string file in files)
                {
                    T gameObject = FileManager.XmlDeserialize<T>(file);
                    gameObjects.Add(gameObject);
                }
            }
            catch
            {
                gameObjects = null;
            }

            return gameObjects;
        }

        public static T GetGameObject<T>(string folderName, string resref) where T: IGameObject
        {
            try
            {
                BindingList<T> gameObjects = GetAllGameObjects<T>(folderName);

                return gameObjects.SingleOrDefault(x => x.Resref == resref);
            }
            catch
            {
                return default(T);
            }
        }

        public static BindingList<string> GetAllScriptNames()
        {
            BindingList<string> scripts = new BindingList<string>();

            try
            {
                string[] scriptNames = Directory.GetFiles(WorkingPaths.ScriptsDirectory, "*.js");
                foreach (string name in scriptNames)
                {
                    scripts.Add(Path.GetFileNameWithoutExtension(name));
                }

            }
            catch
            {
                scripts = null;
            }

            return scripts;
        }

        public static FileOperationResultTypeEnum DeleteScript(string scriptName)
        {
            FileOperationResultTypeEnum result = FileOperationResultTypeEnum.Unknown;

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

        public static bool DoesGameObjectExist(IGameObject gameObject)
        {
            string path = gameObject.WorkingDirectory + gameObject.Resref + EnginePaths.DataExtension;

            return File.Exists(path);
        }

        public static GameModule GetGameModule()
        {
            GameModule module = null;

            try
            {
                string path = EnginePaths.WorkingDirectory + EnginePaths.ModuleDataFileName + EnginePaths.DataExtension;
                module = FileManager.XmlDeserialize<GameModule>(path);

            }
            catch (Exception ex)
            {
                module = null;
            }

            return module;
        }

        public static FileOperationResultTypeEnum SaveModuleSettings(GameModule module)
        {
            FileOperationResultTypeEnum result = FileOperationResultTypeEnum.Unknown;

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
