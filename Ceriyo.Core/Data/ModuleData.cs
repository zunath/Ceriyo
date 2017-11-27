using System;
using System.Collections.Generic;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores module data
    /// </summary>
    public class ModuleData : IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => "Module";

        /// <summary>
        /// The resource packs used by the module.
        /// </summary>
        public List<string> ResourcePacks { get; set; }

        /// <summary>
        /// The name of the module.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The tag of the module.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The resref of the module.
        /// </summary>
        public string Resref { get; set; }

        /// <summary>
        /// The max level of the module.
        /// </summary>
        public int MaxLevel { get; set; }

        /// <summary>
        /// The description of the module.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The comment of the module.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// References to ability objects
        /// </summary>
        public List<string> AbilityIDs { get; private set; }

        /// <summary>
        /// References to class objects.
        /// </summary>
        public List<string> ClassIDs { get; private set; }

        /// <summary>
        /// References to creature objects.
        /// </summary>
        public List<string> CreatureIDs { get; private set; }

        /// <summary>
        /// References to item objects.
        /// </summary>
        public List<string> ItemIDs { get; private set; }

        /// <summary>
        /// References to item property objects.
        /// </summary>
        public List<string> ItemPropertyIDs { get; private set; }

        /// <summary>
        /// References to placeable objects.
        /// </summary>
        public List<string> PlaceableIDs { get; private set; }

        /// <summary>
        /// References to script objects.
        /// </summary>
        public List<string> ScriptIDs { get; private set; }

        /// <summary>
        /// References to skill objects.
        /// </summary>
        public List<string> SkillIDs { get; private set; }

        /// <summary>
        /// References to tileset objects.
        /// </summary>
        public List<string> TilesetIDs { get; private set; }

        /// <summary>
        /// Script resref fired when a player levels up.
        /// </summary>
        public string OnPlayerLevelUp { get; set; }

        /// <summary>
        /// Script resref fired when a player enters the module.
        /// </summary>
        public string OnPlayerEnter { get; set; }

        /// <summary>
        /// Script resref fired when a player is leaving the module.
        /// </summary>
        public string OnPlayerLeaving { get; set; }

        /// <summary>
        /// Script resref fired when a player has already left the module.
        /// </summary>
        public string OnPlayerLeft { get; set; }

        /// <summary>
        /// Script resref fired every heartbeat.
        /// </summary>
        public string OnHeartbeat { get; set; }

        /// <summary>
        /// Script resref fired when the module is loaded.
        /// </summary>
        public string OnModuleLoad { get; set; }

        /// <summary>
        /// Script resref fired when a player starts dying but hasn't fully died yet.
        /// </summary>
        public string OnPlayerDying { get; set; }

        /// <summary>
        /// Script resref fired when a player has died.
        /// </summary>
        public string OnPlayerDeath { get; set; }

        /// <summary>
        /// Script resref fired when a player has respawned.
        /// </summary>
        public string OnPlayerRespawn { get; set; }

        /// <summary>
        /// The local variables stored on the module.
        /// </summary>
        public LocalVariableData LocalVariables { get; set; }

        /// <summary>
        /// The level progression chart.
        /// </summary>
        public LevelChartData LevelChart { get; set; }
        
        /// <summary>
        /// Constructs a new module data object.
        /// </summary>
        public ModuleData()
        {
            GlobalID = Guid.NewGuid().ToString();

            AbilityIDs = new List<string>();
            ClassIDs = new List<string>();
            CreatureIDs = new List<string>();
            ItemIDs = new List<string>();
            ItemPropertyIDs = new List<string>();
            PlaceableIDs = new List<string>();
            ScriptIDs = new List<string>();
            SkillIDs = new List<string>();
            TilesetIDs = new List<string>();
            MaxLevel = 99;
            LocalVariables = new LocalVariableData();
            LevelChart = new LevelChartData();
            ResourcePacks = new List<string>();
        }
    }
}
