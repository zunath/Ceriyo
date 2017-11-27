using System.IO;
using Ceriyo.Core.Data;

namespace Ceriyo.Core.Contracts
{
    /// <summary>
    /// Handles CRUD operations with data files.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Call this once to initialize the data service.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Loads a file into an object. If filePath is null the FilePathAttribute will be used.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize into.</typeparam>
        /// <param name="filePath">The path of the file. If null, the FilePathAttribute will be used.</param>
        /// <returns>The deserialized object.</returns>
        T Load<T>(string filePath = null)
            where T: class;

        /// <summary>
        /// Serializes an object to a file on disk.
        /// </summary>
        /// <param name="obj">The object to serialize</param>
        /// <param name="filePath">The path of the file. If null, the FilePathAttribute will be used.</param>
        void Save(object obj, string filePath = null);

        /// <summary>
        /// Deletes a serialized file at a specified file path.
        /// </summary>
        /// <param name="filePath"></param>
        void Delete(string filePath);

        /// <summary>
        /// Packages a file into a stream.
        /// </summary>
        /// <typeparam name="T">The type of data to package.</typeparam>
        /// <param name="data">The data to package.</param>
        /// <param name="stream">The stream to write to.</param>
        void PackageFile<T>(T data, Stream stream);

        /// <summary>
        /// Retrieves the manifest file from a serialized file.
        /// </summary>
        /// <param name="serializedFilePath">The path to the serialized file.</param>
        /// <returns>The manifest data for a packaged file.</returns>
        SerializedManifestData RetrieveManifest(string serializedFilePath);

        /// <summary>
        /// Retrieves a single file from a serialized package.
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve.</typeparam>
        /// <param name="serializedFilePath">The path to the serialized file.</param>
        /// <param name="key">The unique key of the resource.</param>
        /// <returns>The deserialized file</returns>
        T RetrieveSingleFile<T>(string serializedFilePath, string key)
            where T : class;

        /// <summary>
        /// Packages an entire directory into a single serialized file.
        /// </summary>
        /// <param name="directoryPath">The path to the directory.</param>
        /// <param name="serializedFilePath">The path to the saved serialized file.</param>
        void PackageDirectory(string directoryPath, string serializedFilePath);

        /// <summary>
        /// Unpackages a single file into a directory.
        /// </summary>
        /// <param name="destinationDirectoryPath">The path to the directory.</param>
        /// <param name="sourceFilePath">The path to the serialized file source.</param>
        void UnpackageDirectory(string destinationDirectoryPath, string sourceFilePath);


    }
}
