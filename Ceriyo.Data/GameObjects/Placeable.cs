using System.ComponentModel;
using System.Xml.Serialization;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;
using Ceriyo.Data.ResourceObjects;

namespace Ceriyo.Data.GameObjects
{
    public class Placeable : IGameObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        [XmlIgnore]
        public string WorkingDirectory { get { return WorkingPaths.PlaceablesDirectory; } }
        public BindingList<LocalVariable> LocalVariables { get; set; }
        public SerializableDictionary<ScriptEventType, string> Scripts { get; set; }
        [XmlIgnore]
        public string CategoryName { get { return "Placeable"; } }

        public GameResource Graphic { get; set; }
        public bool IsPlot { get; set; }
        public bool IsUseable { get; set; }
        public bool IsStatic { get; set; }

        public bool IsLocked { get; set; }
        public bool IsKeyRequired { get; set; }
        public bool AutoRemoveKey { get; set; }
        public string KeyTag { get; set; }

        public string DialogResref { get; set; }
        [XmlIgnore]
        public Dialog Dialog 
        {
            get
            {
                return WorkingDataManager.GetGameObject<Dialog>(ModulePaths.DialogsDirectory, DialogResref);
            }
        }

        public Placeable()
        {
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
            Description = string.Empty;
            Comments = string.Empty;
            LocalVariables = new BindingList<LocalVariable>();
            Scripts = new SerializableDictionary<ScriptEventType, string>();
            Graphic = new GameResource();
            
            Scripts.Add(ScriptEventType.OnPlaceableAttacked, "");
            Scripts.Add(ScriptEventType.OnPlaceableClose, "");
            Scripts.Add(ScriptEventType.OnPlaceableDamaged, "");
            Scripts.Add(ScriptEventType.OnPlaceableDeath, "");
            Scripts.Add(ScriptEventType.OnPlaceableDisturbed, "");
            Scripts.Add(ScriptEventType.OnPlaceableHeartbeat, "");
            Scripts.Add(ScriptEventType.OnPlaceableLocked, "");
            Scripts.Add(ScriptEventType.OnPlaceableOpen, "");
            Scripts.Add(ScriptEventType.OnPlaceableUnlocked, "");
            Scripts.Add(ScriptEventType.OnPlaceableUsed, "");
        }
    }
}
