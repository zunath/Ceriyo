﻿using System;
using System.Collections.Generic;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data.Contracts;
using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Core.Services.Module
{
    /// <inheritdoc />
    public class ModuleDataService: IModuleDataService
    {
        private readonly IDataService _dataService;
        private readonly string _moduleDirectory;

        /// <inheritdoc />
        public ModuleDataService(
            IPathService pathService,
            IDataService dataService,
            bool isRunningAsServer)
        {
            _dataService = dataService;

            _moduleDirectory = isRunningAsServer ? 
                pathService.ModulesServerTempDirectory : 
                pathService.ModulesToolsetTempDirectory;
        }

        private static string GetFolderName(Type type)
        {
            if (type.Name.EndsWith("Data"))
            {
                return type.Name.Substring(0, type.Name.Length - 4);
            }

            return type.Name;
        }

        /// <inheritdoc />
        public T Load<T>(string globalID) 
            where T : class, IDataDomainObject
        {
            string folder = GetFolderName(typeof(T));
            string path = $"{_moduleDirectory}{folder}/{globalID}.dat";

            return _dataService.Load<T>(path);
        }

        /// <inheritdoc />
        public IEnumerable<T> LoadAll<T>()
            where T: class, IDataDomainObject
        {
            string folder = GetFolderName(typeof(T));
            string path = $"{_moduleDirectory}{folder}/";
            string[] files = Directory.GetFiles(path, "*.dat");

            foreach (var file in files)
            {
                yield return _dataService.Load<T>(file);
            }
        }
    }
}
