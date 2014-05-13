using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ceriyo.Data.ResourceObjects;
using Ionic.Zip;

namespace Ceriyo.Data.Engine
{
    public class GameResourceProcessor
    {
        public GameResourceProcessor()
        {
        }

        public MemoryStream ToMemoryStream(GameResource resource)
        {
            MemoryStream stream = new MemoryStream();
            string path = EnginePaths.ResourcePacksDirectory + resource.Package + EnginePaths.ResourcePackExtension;

            using (ZipFile zip = new ZipFile(path))
            {
                ZipEntry entry = zip[resource.FileName];
                entry.Extract(stream);
            }

            return stream;
        }
    }
}
