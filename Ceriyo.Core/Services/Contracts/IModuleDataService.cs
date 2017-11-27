using System.Collections.Generic;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Services.Contracts
{
    /// <summary>
    /// Service used for loading module data.
    /// </summary>
    public interface IModuleDataService
    {
        /// <summary>
        /// Loads a file located in the temp0 or temp1 folder into memory.
        /// "temp0" is used when retrieving files for a module loaded in the toolset.
        /// "temp1" is used when retrieving files for a module loaded in the server app.
        /// </summary>
        /// <typeparam name="T">The type of file to load.</typeparam>
        /// <param name="globalID">The GUID of the object you're loading</param>
        /// <returns></returns>
        T Load<T>(string globalID)
            where T : class, IDataDomainObject;

        /// <summary>
        /// Loads all files of a particular type into memory.
        /// Files will be retrieved from the "temp0" folder for a module loaded in the toolset.
        /// Files will be retrieved from the "temp1" folder for a module loaded in the server app.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> LoadAll<T>()
            where T : class, IDataDomainObject;
    }
}
