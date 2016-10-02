﻿using System;
using System.IO;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.Serialization;
using ProtoBuf;

namespace Ceriyo.Infrastructure.Services
{
    public class DataService : IDataService
    {
        private readonly ILogger _logger;

        public DataService(ILogger logger)
        {
            _logger = logger;
        }

        public void Initialize()
        {
            ProtobufContext.Build();
        }

        public T Load<T>(string filePath = null)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                var attribute = (FilePathAttribute) Attribute.GetCustomAttribute(typeof(T), typeof(FilePathAttribute));
                filePath = attribute.FilePath;
            }

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Invalid file path.");

            if (File.Exists(filePath))
            {
                try
                {
                    using (FileStream stream = File.OpenRead(filePath))
                    {
                        return Serializer.Deserialize<T>(stream);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("Unable to load file.", ex);
                    throw new Exception("Unable to load file.", ex);
                }
            }

            return Activator.CreateInstance<T>();
        }

        public void Save<T>(T obj, string filePath = null)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                var attribute = (FilePathAttribute) Attribute.GetCustomAttribute(typeof(T), typeof(FilePathAttribute));
                filePath = attribute.FilePath;
            }

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Invalid file path.");

            var directory = Path.GetDirectoryName(filePath);
            if (directory == null) throw new ArgumentException("Path supplied does not have a valid directory.");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (FileStream stream = File.Create(filePath))
            {
                Serializer.Serialize(stream, obj);
            }
        }

        public void Delete(string filePath)
        {
            if(string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("filePath must not be blank or null.");

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public void PackageDirectory(string directoryPath, string serializedFilePath)
        {
            var destinationDirectory = Path.GetDirectoryName(serializedFilePath);
            if (string.IsNullOrWhiteSpace(destinationDirectory))
                throw new Exception("filePath's destination directory must exist.");

            if (!Directory.Exists(Path.GetDirectoryName(serializedFilePath)))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            using (var stream = File.Create(serializedFilePath))
            {
                foreach (var file in Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories))
                {
                    string cleanedFilePath = file.Replace("\\", "/");
                    if (cleanedFilePath != serializedFilePath)
                    {
                        PackageFile(file, directoryPath, stream);
                    }
                }
            }
        }

        private void PackageFile(string filePath, string directoryPath, Stream stream)
        {
            string trimmedFile = filePath.Replace(directoryPath, string.Empty);
            ModuleFileData fileData = new ModuleFileData
            {
                FilePath = trimmedFile,
                Data = File.ReadAllBytes(filePath)
            };

            Serializer.SerializeWithLengthPrefix(stream, fileData, PrefixStyle.Base128);
        }


        public void UnpackageDirectory(string destinationDirectoryPath, string sourceFilePath)
        {
            try
            {
                if (!Directory.Exists(destinationDirectoryPath))
                {
                    Directory.CreateDirectory(destinationDirectoryPath);
                }

                using (var stream = File.OpenRead(sourceFilePath))
                {
                    foreach (var fileData in Serializer.DeserializeItems<ModuleFileData>(stream, PrefixStyle.Base128, 0))
                    {
                        string destinationPath = destinationDirectoryPath + fileData.FilePath;

                        if (!Directory.Exists(Path.GetDirectoryName(destinationPath)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                        }

                        File.WriteAllBytes(destinationDirectoryPath + fileData.FilePath, fileData.Data);
                    }
                }

            }
            catch (Exception)
            {
                if (Directory.Exists(destinationDirectoryPath))
                {
                    Directory.Delete(destinationDirectoryPath, true);
                }
                throw;
            }

        }
    }
}
