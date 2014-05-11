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

    }
}
