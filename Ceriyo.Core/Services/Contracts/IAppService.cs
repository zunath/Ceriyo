namespace Ceriyo.Core.Services.Contracts
{
    /// <summary>
    /// Methods which affect the entire application.
    /// </summary>
    public interface IAppService
    {
        /// <summary>
        /// Generates the application directory structure.
        /// </summary>
        void CreateAppDirectoryStructure();
    }
}
