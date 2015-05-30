using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Library.Extensions
{
    public static class Texture2DExtensions
    {
        public static BitmapImage ToBitmapImage(this Texture2D texture)
        {
            BitmapImage image = new BitmapImage();
            MemoryStream stream = new MemoryStream();
            texture.SaveAsPng(stream, texture.Width, texture.Height);
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            

            return image;
        }
    }
}
