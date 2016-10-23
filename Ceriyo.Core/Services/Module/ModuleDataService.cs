using System;
using System.Collections.Generic;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data.Contracts;
using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Core.Services.Module
{
    public class ModuleDataService: IModuleDataService
    {
        private readonly IPathService _pathService;
        private readonly IDataService _dataService;

        public ModuleDataService(
            IPathService pathService,
            IDataService dataService)
        {
            _pathService = pathService;
            _dataService = dataService;
        }

        private static string GetFolderName(Type type)
        {
            if (type.Name.EndsWith("Data"))
            {
                return type.Name.Substring(0, type.Name.Length - 4);
            }

            return type.Name;
        }

        public T Load<T>(string globalID) 
            where T : class, IDataDomainObject
        {
            string folder = GetFolderName(typeof(T));
            string path = $"{_pathService.ModulesTempDirectory}{folder}/{globalID}.dat";

            return _dataService.Load<T>(path);
        }

        public IEnumerable<T> LoadAll<T>()
            where T: class, IDataDomainObject
        {
            string folder = GetFolderName(typeof(T));
            string path = $"{_pathService.ModulesTempDirectory}{folder}/";
            string[] files = Directory.GetFiles(path, "*.dat");

            foreach (var file in files)
            {
                yield return _dataService.Load<T>(file);
            }
        }
    }
}
