using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Core.Services
{
    /// <inheritdoc />
    public class PathService: IPathService
    {
        /// <inheritdoc />
        public string ModulesToolsetTempDirectory => "./Modules/temp0/";

        /// <inheritdoc />
        public string ModulesServerTempDirectory => "./Modules/temp1/";

        /// <inheritdoc />
        public string ModuleDirectory => "./Modules/";

        /// <inheritdoc />
        public string ResourcePackDirectory => "./ResourcePacks/";

        /// <inheritdoc />
        public string ServerVaultDirectory => "./ServerVault/";

        /// <inheritdoc />
        public string EngineGraphicsDirectory => "./Graphics/";
    }
}
