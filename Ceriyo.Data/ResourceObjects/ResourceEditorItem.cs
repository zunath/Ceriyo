using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ceriyo.Data.ResourceObjects
{
    public class ResourceEditorItem
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public long SizeBytes { get; set; }
        [XmlIgnore]
        public byte[] Contents { get; set; }

        public ResourceEditorItem()
        {
            this.FileName = string.Empty;
            this.Extension = string.Empty;
            this.SizeBytes = 0;
        }

        public ResourceEditorItem(string fileName, string extension, long sizeBytes)
        {
            this.FileName = fileName;
            this.Extension = extension;
            this.SizeBytes = sizeBytes;
        }
    }
}
