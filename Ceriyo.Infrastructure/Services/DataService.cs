using System;
using System.IO;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Contracts;
using Newtonsoft.Json;

namespace Ceriyo.Infrastructure.Services
{
    public class DataService : IDataService
    {
        private readonly ILogger _logger;

        public DataService(ILogger logger)
        {
            _logger = logger;
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
    }
}
