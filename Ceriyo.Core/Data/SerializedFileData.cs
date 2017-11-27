namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores serialized file data.
    /// </summary>
    public class SerializedFileData
    {
        /// <summary>
        /// The file path of the serialized file data.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The raw data 
        /// </summary>
        public byte[] Data { get; set; }
    }
}
