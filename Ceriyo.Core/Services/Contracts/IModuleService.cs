using System.Collections.Generic;
using Ceriyo.Core.Data;

namespace Ceriyo.Core.Services.Contracts
{
    public interface IModuleService
    {
        void CreateModule(string name, string tag, string resref);
        void SaveModuleProperties();
        void CloseModule();
        void OpenModule(string fileName);
        void PackModule(string fileName);
        void ReplaceResourcePacks(IEnumerable<string> resourcePacks);
        void UpdateLoadedModuleData(ModuleData moduleData);
        ModuleData GetLoadedModuleData();
    }
}
