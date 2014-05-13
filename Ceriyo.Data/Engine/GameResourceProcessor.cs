using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;
using Ionic.Zip;

namespace Ceriyo.Data.Engine
{
    public class GameResourceProcessor
    {
        public GameResourceProcessor()
        {
        }

        public string GenerateUniqueResref(IGameObject gameObject)
        {
            string resref = gameObject.CategoryName;
            string[] files = Directory.GetFiles(gameObject.WorkingDirectory);

            int count = 0;
            foreach(string file in files)
            {
                if (Path.GetFileNameWithoutExtension(file) == resref + count)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            resref = resref + count;

            return resref;
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
