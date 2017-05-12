using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Core.Services
{
    public class PathService: IPathService
    {
        public string ModulesToolsetTempDirectory => "./Modules/temp0/";
        public string ModulesServerTempDirectory => "./Modules/temp1/";
        public string ModuleDirectory => "./Modules/";
        public string ResourcePackDirectory => "./ResourcePacks/";
        public string ServerVaultDirectory => "./ServerVault/";
        public string EngineGraphicsDirectory => "./Graphics/";
    }
}
