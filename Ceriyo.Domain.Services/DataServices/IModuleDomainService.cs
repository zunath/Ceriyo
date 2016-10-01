namespace Ceriyo.Domain.Services.DataServices
{
    public interface IModuleDomainService
    {
        void CreateModule(string name, string tag, string resref);
        void SaveModuleProperties();
    }
}
