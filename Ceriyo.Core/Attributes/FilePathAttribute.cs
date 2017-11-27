using System;

namespace Ceriyo.Core.Attributes
{
    /// <summary>
    /// Identifies a file path where a class will reside when serialized.
    /// </summary>
    public class FilePathAttribute: Attribute
    {
        /// <summary>
        /// The file path to save the file to.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Constructs a FilePathAttribute
        /// </summary>
        /// <param name="filePath">The file path to save the file to.</param>
        public FilePathAttribute(string filePath)
        {
            FilePath = filePath;
        }
    }
}
