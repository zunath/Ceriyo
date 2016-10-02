namespace Ceriyo.Domain.Services.DataServices.Contracts
{
    public interface IModuleDomainService
    {
        void CreateModule(string name, string tag, string resref);
        void SaveModuleProperties();
        void CloseModule();
        void OpenModule(string fileName);
        void PackModule(string fileName);

    }
}
