using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;
using Ceriyo.Data.ResourceObjects;

namespace Ceriyo.Data.GameObjects
{
    public class Tileset : IGameObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        [XmlIgnore]
        public string WorkingDirectory { get { return WorkingPaths.TilesetsDirectory; } }
        public BindingList<LocalVariable> LocalVariables { get; set; }
        [XmlIgnore]
        public SerializableDictionary<ScriptEventTypeEnum, string> Scripts
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }
        public string CategoryName { get { return "Tileset"; } }
        public GameResource Graphic { get; set; }
        public BindingList<TileDefinition> Tiles { get; set; }

        public Tileset()
        {
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
            Description = string.Empty;
            Comments = string.Empty;
            LocalVariables = new BindingList<LocalVariable>();
            Graphic = new GameResource();
            Tiles = new BindingList<TileDefinition>();
        }
    }
}
