using System;
using System.IO;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Contracts;
using Newtonsoft.Json;

namespace Ceriyo.Infrastructure.Services
{
    public class DataService: IDataService
    {
        public T Load<T>(string filePath = null)
            where T: class
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                var attribute = (FilePathAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(FilePathAttribute));
                filePath = attribute.FilePath;
            }

            if(string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Invalid file path.");

            return File.Exists(filePath) ?
                JsonConvert.DeserializeObject<T>(filePath) :
                Activator.CreateInstance<T>();
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
            
            string json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(filePath, json);
        }
    }
}
