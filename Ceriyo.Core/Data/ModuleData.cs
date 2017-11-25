using System;
using System.Collections.Generic;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class ModuleData : IDataDomainObject
    {
        public List<string> ResourcePacks { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public string Resref { get; set; }

        public int MaxLevel { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public List<string> AbilityIDs { get; private set; }

        public List<string> ClassIDs { get; private set; }

        public List<string> CreatureIDs { get; private set; }

        public List<string> ItemIDs { get; private set; }

        public List<string> ItemPropertyIDs { get; private set; }

        public List<string> PlaceableIDs { get; private set; }

        public List<string> ScriptIDs { get; private set; }

        public List<string> SkillIDs { get; private set; }

        public List<string> TilesetIDs { get; private set; }

        public string OnPlayerLevelUp { get; set; }

        public string OnPlayerEnter { get; set; }

        public string OnPlayerLeaving { get; set; }

        public string OnPlayerLeft { get; set; }

        public string OnHeartbeat { get; set; }

        public string OnModuleLoad { get; set; }

        public string OnPlayerDying { get; set; }

        public string OnPlayerDeath { get; set; }

        public string OnPlayerRespawn { get; set; }

        public LocalVariableData LocalVariables { get; set; }

        public LevelChartData LevelChart { get; set; }

        public string GlobalID { get; set; }
        [SerializationIgnore]
        public string DirectoryName => "Module";

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
