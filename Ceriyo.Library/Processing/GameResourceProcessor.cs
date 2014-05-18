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
using Microsoft.Xna.Framework.Graphics;
using FlatRedBall;
using Microsoft.Xna.Framework;

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

            BitmapImage image = null;
            if (resource.ResourceType != ResourceTypeEnum.None)
            {
                image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(ToBytes(resource));
                image.EndInit();
            }
            return image;
        }

        public Texture2D ToTexture2D(GameResource resource)
        {
            byte[] imageData = ToBytes(resource);

            return Texture2D.FromStream(FlatRedBallServices.GraphicsDevice, new MemoryStream(imageData));
        }

        public Texture2D GetSubTexture(GameResource resource, int x, int y, int width, int height)
        {
            Texture2D fullTexture = ToTexture2D(resource);
            Texture2D texture = new Texture2D(FlatRedBallServices.GraphicsDevice, width, height);
            Rectangle rect = new Rectangle(x, y, width, height);

            Color[] data = new Color[width * height];
            fullTexture.GetData<Color>(0, rect, data, 0, data.Length);
            texture.SetData<Color>(data);

            return texture;
        }

    }
}
