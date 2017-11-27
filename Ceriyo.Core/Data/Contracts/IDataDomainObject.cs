namespace Ceriyo.Core.Data.Contracts
{
    /// <summary>
    /// Identifies an object as being part of the "Data Domain".
    /// This means the object will have a global ID (GUID) and optionally a directory name used for serialization.
    /// It's up to the implementor to decide if a DirectoryName is necessary, but the GlobalID is always required.
    /// </summary>
    public interface IDataDomainObject
    {
        /// <summary>
        /// Globally unique identifier for the domain object.
        /// </summary>
        string GlobalID { get; set; }

        /// <summary>
        /// Name of the directory in which data of this type resides.
        /// </summary>
        string DirectoryName { get; }
    }
}
