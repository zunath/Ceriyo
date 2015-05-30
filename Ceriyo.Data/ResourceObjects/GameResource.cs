using System;
using System.IO;
using Ceriyo.Data.Enumerations;
using FlatRedBall;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Ceriyo.Data.ResourceObjects
{
    public class GameResource
    {
        public string Name { get { return Path.GetFileNameWithoutExtension(FileName); } }
        public string Package { get; set; }
        public string FileName { get; set; }
        public ResourceType ResourceType { get; set; }
        public ResourceSubType ResourceSubType { get; set; }
        public string Contents { get; set; }

        public GameResource()
        {
            FileName = string.Empty;
            Package = string.Empty;
            ResourceType = ResourceType.Unknown;
            ResourceSubType = ResourceSubType.Unknown;
            Contents = string.Empty;
        }

        public GameResource(string package, 
            string fileName, 
            ResourceType resourceType, 
            ResourceSubType resourceSubType)
        {
            FileName = fileName;
            Package = package;
            ResourceType = resourceType;
            ResourceSubType = resourceSubType;
        }

        private byte[] ToBytes()
        {
            return Convert.FromBase64String(Contents);
        }

        public Texture2D ToTexture2D()
        {
            if(ResourceType != ResourceType.Graphic)
                throw new Exception("Only graphics can be converted to Texture2D");

            return Texture2D.FromStream(FlatRedBallServices.GraphicsDevice, new MemoryStream(ToBytes()));
        }

        public Texture2D GetSubTexture(int x, int y, int width, int height)
        {
            if (ResourceType != ResourceType.Graphic)
                throw new Exception("Only graphics can be converted to Texture2D");

            Texture2D fullTexture = ToTexture2D();
            Texture2D texture = new Texture2D(FlatRedBallServices.GraphicsDevice, width, height);
            Rectangle rect = new Rectangle(x, y, width, height);

            Color[] data = new Color[width * height];
            fullTexture.GetData(0, rect, data, 0, data.Length);
            texture.SetData(data);

            return texture;
        }


    }
}
