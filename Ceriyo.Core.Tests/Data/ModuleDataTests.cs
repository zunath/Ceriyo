using Ceriyo.Core.Data;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Data
{
    public class ModuleDataTests
    {
        [Test]
        public void ModuleData_OnInstantiate_ShouldCreateGlobalID()
        {
            ModuleData data = new ModuleData();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(data.GlobalID));
        }

        [Test]
        public void ModuleData_OnInstantiate_ShouldCreateIDLists()
        {
            ModuleData data = new ModuleData();
            Assert.IsNotNull(data.AbilityIDs);
            Assert.IsNotNull(data.ClassIDs);
            Assert.IsNotNull(data.CreatureIDs);
            Assert.IsNotNull(data.ItemIDs);
            Assert.IsNotNull(data.ItemPropertyIDs);
            Assert.IsNotNull(data.PlaceableIDs);
            Assert.IsNotNull(data.ScriptIDs);
            Assert.IsNotNull(data.SkillIDs);
            Assert.IsNotNull(data.TilesetIDs);
        }

        [Test]
        public void ModuleData_OnInstantiate_ShouldCreateLocalVariables()
        {
            ModuleData data = new ModuleData();
            Assert.IsNotNull(data.LocalVariables);
        }

        [Test]
        public void ModuleData_OnInstantiate_ShouldCreateLevelChart()
        {
            ModuleData data = new ModuleData();
            Assert.IsNotNull(data.LevelChart);
        }

        [Test]
        public void ModuleData_OnInstantiate_ShouldCreateResourcePacks()
        {
            ModuleData data = new ModuleData();
            Assert.IsNotNull(data.ResourcePacks);
        }
    }
}
