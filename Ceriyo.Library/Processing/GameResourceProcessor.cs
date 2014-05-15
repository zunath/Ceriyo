using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;
using Ionic.Zip;
using Ceriyo.Data;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using Ceriyo.Data.Enumerations;

namespace Ceriyo.Library.Processing
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

        public string GenerateUniqueResref(IList<IGameObject> gameObjectList, string defaultCategoryName = "")
        {
            int count = 0;
            string resref = defaultCategoryName + count;

            if (gameObjectList.Count > 0)
            {
                resref = gameObjectList[0].CategoryName;

                while (gameObjectList.FirstOrDefault(x => x.Resref == resref + count) != null)
                {
                    count++;
                }

                resref = resref + count;
            }

            return resref;
        }

        public byte[] ToBytes(GameResource resource)
        {
            string path = EnginePaths.ResourcePacksDirectory + resource.Package;
            byte[] bytes;

            using (MemoryStream stream = new MemoryStream())
            {
                using (ZipFile zip = new ZipFile(path))
                {
                    ZipEntry entry = zip[resource.FileName];
                    entry.Extract(stream);
                }
                stream.Flush();
                bytes = stream.ToArray();
            }

            return bytes;
        }

        public BitmapImage ToBitmapImage(GameResource resource)
        {

            BitmapImage image = new BitmapImage();
            if (resource.ResourceType != ResourceTypeEnum.None)
            {
                image.BeginInit();
                image.StreamSource = new MemoryStream(ToBytes(resource));
                image.EndInit();
            }
            return image;
        }

    }
}
