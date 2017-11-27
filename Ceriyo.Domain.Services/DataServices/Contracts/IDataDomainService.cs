using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Domain.Services.DataServices.Contracts
{
    /// <summary>
    /// Service which handles saving/deleting files in the temporary directories.
    /// </summary>
    public interface IDataDomainService
    {
        /// <summary>
        /// Saves an object to the temporary module directory.
        /// </summary>
        /// <typeparam name="T">The type of data to save.</typeparam>
        /// <param name="data">The actual data to save.</param>
        void SaveData<T>(T data)
            where T: IDataDomainObject;

        /// <summary>
        /// Deletes a file from the temporary module directory.
        /// </summary>
        /// <typeparam name="T">The type of data to delete.</typeparam>
        /// <param name="data">The actual data to delete.</param>
        void DeleteData<T>(T data)
            where T: IDataDomainObject;
    }
}
