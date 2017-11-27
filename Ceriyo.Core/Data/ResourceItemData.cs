using Ceriyo.Core.Constants;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores resource item data.
    /// Resources are content stored in resource packages like graphics and sounds.
    /// </summary>
    public class ResourceItemData
    {
        /// <summary>
        /// The file path of the resource item.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The file name of the resource item.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The file extension of the resource item.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// The size, in bytes, of the resource item.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// The type of resource.
        /// </summary>
        public ResourceType ResourceType { get; set; }

        /// <summary>
        /// The raw file data of the resource.
        /// </summary>
        public byte[] Data { get; set; }
    }
}
