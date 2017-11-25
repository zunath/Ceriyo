using System.IO;
using Ceriyo.Core.Data;

namespace Ceriyo.Core.Contracts
{
    public interface IDataService
    {
        void Initialize();

        T Load<T>(string filePath = null)
            where T: class;

        void Save(object obj, string filePath = null);

        void Delete(string filePath);

        void PackageFile<T>(T data, Stream stream);
        SerializedManifestData RetrieveManifest(string serializedFilePath);

        T RetrieveSingleFile<T>(string serializedFilePath, string key)
            where T : class;

        void PackageDirectory(string directoryPath, string serializedFilePath);
        void UnpackageDirectory(string destinationDirectoryPath, string sourceFilePath);


    }
}
