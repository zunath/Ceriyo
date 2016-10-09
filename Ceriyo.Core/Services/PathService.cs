using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Core.Services
{
    public class PathService: IPathService
    {
        public string ModulesTempDirectory => "./Modules/temp0/";
        public string ModuleDirectory => "./Modules/";
        public string ResourcePackDirectory => "./ResourcePacks/";
        public string ServerVaultDirectory => "./ServerVault/";
    }
}
