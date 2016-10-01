namespace Ceriyo.Core.Contracts
{
    public interface IDataService
    {
        void Initialize();

        T Load<T>(string filePath = null)
            where T: class;

        void Save<T>(T obj, string filePath = null)
            where T : class;

        void PackageDirectory(string directoryPath, string serializedFilePath);
        void UnpackageDirectory(string destinationDirectoryPath, string filePath);
    }
}
