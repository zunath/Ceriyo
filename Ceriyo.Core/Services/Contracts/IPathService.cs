namespace Ceriyo.Core.Services.Contracts
{
    public interface IPathService
    {
        string ModulesTempDirectory { get; }
        string ModuleDirectory { get; }
        string ResourcePackDirectory { get; }
        string ServerVaultDirectory { get; }
        string EngineGraphicsDirectory { get; }
    }
}
