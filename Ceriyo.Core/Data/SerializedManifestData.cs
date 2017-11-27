using System.Collections.Generic;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Key-value pair of where files are located in a serialized package.
    /// Key is the name of the file.
    /// Value is the index of where that file is located in the serialized package.
    /// </summary>
    public class SerializedManifestData: Dictionary<string, int>
    {
    }
}
