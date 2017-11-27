using System.Collections.Generic;
using Ceriyo.Core.Data;

namespace Ceriyo.Core.Services.Contracts
{
    /// <summary>
    /// Service used for managing module specific data.
    /// </summary>
    public interface IModuleService
    {
        /// <summary>
        /// Creates a new module using the specified arguments.
        /// The project structure and initial data will be created as part of this process.
        /// </summary>
        /// <param name="name">The name of the module</param>
        /// <param name="tag">The tag of the module</param>
        /// <param name="resref">The resref of the module</param>
        void CreateModule(string name, string tag, string resref);
        /// <summary>
        /// Saves the module properties to disk. These are not permanently saved until PackModule is called.
        /// </summary>
        void SaveModuleProperties();
        /// <summary>
        /// Cleans up all temporary files related to an open module from the temp directory.
        /// </summary>
        void CloseModule();
        /// <summary>
        /// Unpacks a .mod file into the temporary directory.
        /// </summary>
        /// <param name="fileName">The name of the mod file to unpack. No extension should be provided.</param>
        void OpenModule(string fileName);
        /// <summary>
        /// Serializes all temporary module files on disk into a single mod file.
        /// </summary>
        /// <param name="fileName">The name of the file to pack files into.</param>
        void PackModule(string fileName);
        /// <summary>
        /// Clears all existing resource packs and attaches the new set.
        /// A module build will subsequently occur.
        /// </summary>
        /// <param name="resourcePacks">The new resource packs to attach.</param>
        void ReplaceResourcePacks(IEnumerable<string> resourcePacks);
        /// <summary>
        /// Refreshes the cached module data with the moduleData argument's version.
        /// </summary>
        /// <param name="moduleData">The new data to store in the cache.</param>
        void UpdateLoadedModuleData(ModuleData moduleData);
        /// <summary>
        /// Returns a COPY of the cached module data.
        /// Changes to the module data should be applied via the UpdateLoadedModuleData method.
        /// </summary>
        /// <returns></returns>
        ModuleData GetLoadedModuleData();
    }
}
