using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
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
        public SerializableDictionary<ScriptEventTypeEnum, string> Scripts { get; set; }
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
            this.Name = "";
            this.Tag = "";
            this.Resref = "";
            this.Description = "";
            this.Comments = "";
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
            this.Graphic = new GameResource();

            Scripts.Add(ScriptEventTypeEnum.OnPlaceableAttacked, "");
            Scripts.Add(ScriptEventTypeEnum.OnPlaceableClose, "");
            Scripts.Add(ScriptEventTypeEnum.OnPlaceableDamaged, "");
            Scripts.Add(ScriptEventTypeEnum.OnPlaceableDeath, "");
            Scripts.Add(ScriptEventTypeEnum.OnPlaceableDisturbed, "");
            Scripts.Add(ScriptEventTypeEnum.OnPlaceableHeartbeat, "");
            Scripts.Add(ScriptEventTypeEnum.OnPlaceableLocked, "");
            Scripts.Add(ScriptEventTypeEnum.OnPlaceableOpen, "");
            Scripts.Add(ScriptEventTypeEnum.OnPlaceableUnlocked, "");
            Scripts.Add(ScriptEventTypeEnum.OnPlaceableUsed, "");
        }
    }
}
