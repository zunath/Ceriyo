using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;
using FlatRedBall;
using Ionic.Zip;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Ceriyo.Data.Engine;

namespace Ceriyo.Library.Processing
{
    public static class GameResourceProcessor
    {
        public static string GenerateUniqueResref(IList<IGameObject> gameObjectList, string defaultCategoryName = "")
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

        private static byte[] ToBytes(GameResource resource)
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

        public static BitmapImage ToBitmapImage(GameResource resource)
        {

            BitmapImage image = null;
            if (resource.ResourceType != ResourceType.None)
            {
                image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(ToBytes(resource));
                image.EndInit();
            }
            return image;
        }

        public static BitmapImage ToBitmapImage(Texture2D texture)
        {
            MemoryStream stream = new MemoryStream();

            texture.SaveAsPng(stream, texture.Width, texture.Height);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();

            return image;
        }

        public static Texture2D ToTexture2D(GameResource resource)
        {
            Texture2D result = null;

            if (resource != null && 
                resource.ResourceType == ResourceType.Graphic)
            {
                byte[] imageData = ToBytes(resource);

                result = Texture2D.FromStream(FlatRedBallServices.GraphicsDevice, new MemoryStream(imageData));
            }

            return result;
        }

        public static Texture2D GetSubTexture(GameResource resource, int x, int y, int width, int height)
        {
            Texture2D fullTexture = ToTexture2D(resource);
            Texture2D texture = new Texture2D(FlatRedBallServices.GraphicsDevice, width, height);
            Rectangle rect = new Rectangle(x, y, width, height);

            Color[] data = new Color[width * height];
            fullTexture.GetData(0, rect, data, 0, data.Length);
            texture.SetData(data);

            return texture;
        }
    }
}
