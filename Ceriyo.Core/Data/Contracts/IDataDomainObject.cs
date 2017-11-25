namespace Ceriyo.Core.Data.Contracts
{
    /// <summary>
    /// 
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
