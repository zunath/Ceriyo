using System;
using System.IO;
using System.Linq;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Entities;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Settings;
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
            // Settings
            Map<ToolsetSettings>();
            Map<ServerSettings>();
            Map<GameSettings>();

            // Data
            Map<AbilityData>();
            Map<AnimationData>();
            Map<ClassData>();
            Map<ClassRequirementData>();
            Map<ClassLevel>();
            Map<CreatureData>();
            Map<DialogData>();
            Map<FrameData>();
            Map<ItemData>();
            Map<ItemPropertyData>();
            Map<ItemTypeData>();
            Map<LocalStringData>();
            Map<LocalDoubleData>();
            Map<LocalVariableData>();
            Map<ModuleData>();
            Map<SerializedFileData>();
            Map<SerializedManifestData>();
            Map<PlaceableData>();
            Map<ResourceItemData>();
            Map<ScriptData>();
            Map<SkillData>();
            Map<TilesetData>();
        }

        private static void Map<T>()
            where T : class
        {
            Type type = typeof(T);
            var registeredTypes = RuntimeTypeModel.Default.GetTypes();

            // Don't register the same type twice.
            foreach (MetaType registeredType in registeredTypes)
            {
                if (registeredType.Type == type) return;
            }


            var meta = RuntimeTypeModel.Default.Add(type, false);
            var properties = type.GetProperties();

            for (int x = 0; x < properties.Length; x++)
            {
                var prop = properties[x];

                if (prop.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(SerializationIgnoreAttribute)) == null)
                {
                    meta.Add(x + 1, prop.Name);
                }
            }

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
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException($"{nameof(filePath)} must not be blank or null.");

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
            SerializedFileData fileData = new SerializedFileData
            {
                FilePath = trimmedFile,
                Data = File.ReadAllBytes(filePath)
            };

            Serializer.SerializeWithLengthPrefix(stream, fileData, PrefixStyle.Base128);
        }

        public void PackageFile<T>(T data, Stream stream)
        {
            SerializedManifestData manifest = null;
            if (stream.Length > 0)
            {
                try
                {
                    long currentPosition = stream.Position;
                    stream.Position = 0;
                    manifest = Serializer.DeserializeWithLengthPrefix<SerializedManifestData>(stream, PrefixStyle.Base128, 0);
                    if (manifest == null)
                    {
                        throw new Exception($"An instance of {nameof(SerializedManifestData)} must be the first packaged file.");
                    }

                    stream.Position = currentPosition;

                }
                catch (Exception ex)
                {
                    throw new Exception($"An instance of {nameof(SerializedManifestData)} must be the first packaged file.", ex);
                }
            }
            else
            {
                if (data.GetType() != typeof(SerializedManifestData))
                {
                    throw new Exception($"An instance of {nameof(SerializedManifestData)} must be the first packaged file.");
                }
            }

            int fieldNumber = 0;
            if (manifest != null)
                fieldNumber = 1;

            Serializer.SerializeWithLengthPrefix(stream, data, PrefixStyle.Base128, fieldNumber);
            
        }

        public SerializedManifestData RetrieveManifest(string serializedFilePath)
        {
            using (var stream = File.OpenRead(serializedFilePath))
            {
                return Serializer.DeserializeWithLengthPrefix<SerializedManifestData>(stream, PrefixStyle.Base128, 0);
            }
        }

        public T RetrieveSingleFile<T>(string serializedFilePath, string key)
            where T: class
        {
            using (var stream = File.OpenRead(serializedFilePath))
            {
                var manifest = Serializer.DeserializeWithLengthPrefix<SerializedManifestData>(stream, PrefixStyle.Base128, 0);

                // TODO: There's room for improvement here. Need to figure out how to access a specific object in Protobuf's deserialization process.
                // TODO: Possible solution involves tracking the size of each object on serialization and jumping the stream to that position.
                for (int index = 0; index <= manifest[key]; index++)
                {
                    var result = Serializer.DeserializeWithLengthPrefix<T>(stream, PrefixStyle.Base128, 1);

                    if (index == manifest[key])
                    {
                        return result;
                    }
                }
            }

            return null;
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
                    foreach (var fileData in Serializer.DeserializeItems<SerializedFileData>(stream, PrefixStyle.Base128, 0))
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
