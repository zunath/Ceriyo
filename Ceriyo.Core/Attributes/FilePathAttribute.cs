using System;

namespace Ceriyo.Core.Attributes
{
    public class FilePathAttribute: Attribute
    {
        public string FilePath { get; set; }

        public FilePathAttribute(string filePath)
        {
            FilePath = filePath;
        }
    }
}
