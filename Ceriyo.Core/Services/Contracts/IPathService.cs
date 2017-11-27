namespace Ceriyo.Core.Services.Contracts
{
    /// <summary>
    /// Service which stores the paths to engine directories.
    /// </summary>
    public interface IPathService
    {
        /// <summary>
        /// The temporary directory for toolset modules.
        /// </summary>
        string ModulesToolsetTempDirectory { get; }

        /// <summary>
        /// The temporary directory for server modules.
        /// </summary>
        string ModulesServerTempDirectory { get; }

        /// <summary>
        /// The permanent modules directory.
        /// </summary>
        string ModuleDirectory { get; }

        /// <summary>
        /// The permanent resource packs directory.
        /// </summary>
        string ResourcePackDirectory { get; }

        /// <summary>
        /// The permanent server vault directory.
        /// </summary>
        string ServerVaultDirectory { get; }

        /// <summary>
        /// The permanent engine graphics directory.
        /// </summary>
        string EngineGraphicsDirectory { get; }
    }
}
