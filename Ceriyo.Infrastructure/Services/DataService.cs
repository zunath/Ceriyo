using System;
using System.IO;
using System.Threading;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
using ProtoBuf.Meta;

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
                    string json = File.ReadAllText(filePath);
                    return JsonConvert.DeserializeObject<T>(json);
                }
                catch (Exception ex)
                {
                    _logger.Error("Unable to load server settings file. Defaulting to normal settings.", ex);
                }
            }
            
            return Activator.CreateInstance<T>();
        }

        public void Save<T>(T obj, string filePath = null) 
            where T : class
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                var attribute = (FilePathAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(FilePathAttribute));
                filePath = attribute.FilePath;
            }

            if(string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Invalid file path.");

            var directory = Path.GetDirectoryName(filePath);
            if(directory == null) throw new ArgumentException("Path supplied does not have a valid directory.");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(filePath, json);
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


        public void UnpackageDirectory(string destinationDirectoryPath, string filePath)
        {
            
        }
    }
}
