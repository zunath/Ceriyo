using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using FlatRedBall;
using FlatRedBall.IO;
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

        [XmlIgnore]
        private string _contents;
        [XmlIgnore]
        public string Contents
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_contents))
                {
                    string path = EnginePaths.ResourcePacksDirectory + Package;
                    List<CPResource> resources = FileManager.XmlDeserialize<List<CPResource>>(path);
                    CPResource resource = resources.Single(x => x.FileName == FileName && 
                        x.ResourceType == ResourceType && 
                        x.ResourceSubType == ResourceSubType);

                    _contents = resource.Contents;
                }
                return _contents;
            }
        }
        [XmlIgnore]
        public byte[] ContentBytes
        {
            get { return Convert.FromBase64String(Contents); }
        }

        public GameResource()
        {
            FileName = string.Empty;
            Package = string.Empty;
            ResourceType = ResourceType.Unknown;
            ResourceSubType = ResourceSubType.Unknown;
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

        public Texture2D ToTexture2D()
        {
            if(ResourceType != ResourceType.Graphic)
                throw new Exception("Only graphics can be converted to Texture2D");

            return Texture2D.FromStream(FlatRedBallServices.GraphicsDevice, new MemoryStream(ContentBytes));
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
