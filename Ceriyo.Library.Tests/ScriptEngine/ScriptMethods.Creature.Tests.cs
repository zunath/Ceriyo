
using System.Collections.Generic;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Server;
using Ceriyo.Library.ScriptEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ceriyo.Library.Tests.ScriptEngine
{
    public partial class ScriptMethodsTests
    {
        #region GetLevel tests

        [TestMethod]
        public void GetLevel_NullEqualsNegative1()
        {
            ScriptMethods methods = new ScriptMethods();
            int result = methods.GetLevel(null);
            Assert.AreEqual(result, -1);
        }

        [TestMethod]
        public void GetLevel_Equals10()
        {
            ScriptMethods methods = new ScriptMethods();
            Creature creature = new Creature();
            creature.Level = 10;
            int result = methods.GetLevel(creature);
            Assert.AreEqual(result, 10);
        }

        [TestMethod]
        public void GetLevel_DoesNotEqual20()
        {
            ScriptMethods methods = new ScriptMethods();
            Creature creature = new Creature();
            creature.Level = 10;
            int result = methods.GetLevel(creature);
            Assert.AreNotEqual(result, 20);
        }

        #endregion

        #region GetCurrentHitPoints tests

        [TestMethod]
        public void GetCurrentHitPoints_NullEqualsNegative1()
        {
            ScriptMethods methods = new ScriptMethods();
            int result = methods.GetCurrentHitPoints(null);
            Assert.AreEqual(result, -1);
        }

        [TestMethod]
        public void GetCurrentHitPoints_Equals10()
        {
            ScriptMethods methods = new ScriptMethods();
            Creature creature = new Creature();
            creature.HitPoints = 10;
            int result = methods.GetCurrentHitPoints(creature);
            Assert.AreEqual(result, 10);
        }

        [TestMethod]
        public void GetCurrentHitPoints_DoesNotEqual20()
        {
            ScriptMethods methods = new ScriptMethods();
            Creature creature = new Creature();
            creature.HitPoints = 10;
            int result = methods.GetCurrentHitPoints(creature);
            Assert.AreNotEqual(result, 20);
        }

        #endregion

        #region GetCurrentMana tests

        [TestMethod]
        public void GetCurrentMana_NullEqualsNegative1()
        {
            ScriptMethods methods = new ScriptMethods();
            int result = methods.GetCurrentMana(null);
            Assert.AreEqual(result, -1);
        }

        [TestMethod]
        public void GetCurrentMana_Equals10()
        {
            ScriptMethods methods = new ScriptMethods();
            Creature creature = new Creature();
            creature.Mana = 10;
            int result = methods.GetCurrentMana(creature);
            Assert.AreEqual(result, 10);
        }

        [TestMethod]
        public void GetCurrentMana_DoesNotEqual20()
        {
            ScriptMethods methods = new ScriptMethods();
            Creature creature = new Creature();
            creature.Mana = 10;
            int result = methods.GetCurrentMana(creature);
            Assert.AreNotEqual(result, 20);
        }

        #endregion

        #region GetAttributeLevel tests

        [TestMethod]
        public void GetAttributeLevel_StrengthEquals10()
        {
            ScriptMethods methods = new ScriptMethods();
            Creature creature = new Creature();
            creature.Strength = 10;
            int result = methods.GetAttributeLevel(creature, AttributeType.Strength);
            Assert.AreEqual(result, 10);
        }

        [TestMethod]
        public void GetAttributeLevel_DexterityEquals10()
        {
            ScriptMethods methods = new ScriptMethods();
            Creature creature = new Creature();
            creature.Dexterity = 10;
            int result = methods.GetAttributeLevel(creature, AttributeType.Dexterity);
            Assert.AreEqual(result, 10);
        }

        [TestMethod]
        public void GetAttributeLevel_ConstitutionEquals10()
        {
            ScriptMethods methods = new ScriptMethods();
            Creature creature = new Creature();
            creature.Constitution = 10;
            int result = methods.GetAttributeLevel(creature, AttributeType.Constitution);
            Assert.AreEqual(result, 10);
        }

        [TestMethod]
        public void GetAttributeLevel_WisdomEquals10()
        {
            ScriptMethods methods = new ScriptMethods();
            Creature creature = new Creature();
            creature.Wisdom = 10;
            int result = methods.GetAttributeLevel(creature, AttributeType.Wisdom);
            Assert.AreEqual(result, 10);
        }

        [TestMethod]
        public void GetAttributeLevel_IntelligenceEquals10()
        {
            ScriptMethods methods = new ScriptMethods();
            Creature creature = new Creature();
            creature.Intelligence = 10;
            int result = methods.GetAttributeLevel(creature, AttributeType.Intelligence);
            Assert.AreEqual(result, 10);
        }

        [TestMethod]
        public void GetAttributeLevel_CharismaEquals10()
        {
            ScriptMethods methods = new ScriptMethods();
            Creature creature = new Creature();
            creature.Charisma = 10;
            int result = methods.GetAttributeLevel(creature, AttributeType.Charisma);
            Assert.AreEqual(result, 10);
        }


        [TestMethod]
        public void GetAttributeLevel_UnknownEqualsNegative1()
        {
            ScriptMethods methods = new ScriptMethods();
            Creature creature = new Creature
            {
                Strength = 10,
                Constitution = 11,
                Wisdom = 12,
                Dexterity = 13,
                Charisma = 14,
                Intelligence = 15
            };

            int result = methods.GetAttributeLevel(creature, AttributeType.Unknown);
            Assert.AreEqual(result, -1);
        }

        [TestMethod]
        public void GetAttributeLevel_NullEqualsNegative1()
        {
            ScriptMethods methods = new ScriptMethods();
            int result = methods.GetAttributeLevel(null, AttributeType.Charisma);
            Assert.AreEqual(result, -1);
        }

        #endregion

        #region GetGender tests

        [TestMethod]
        public void GetGender_NullEqualsUnknown()
        {
            ScriptMethods methods = new ScriptMethods();
            GenderType result = methods.GetGender(null);
            Assert.AreEqual(result, GenderType.Unknown);
        }

        [TestMethod]
        public void GetGender_EqualsMale()
        {
            ScriptMethods methods = new ScriptMethods();
            Creature creature = new Creature();
            creature.Gender = GenderType.Male;
            GenderType result = methods.GetGender(creature);
            Assert.AreEqual(result, GenderType.Male);
        }

        [TestMethod]
        public void GetGender_EqualsFemale()
        {
            ScriptMethods methods = new ScriptMethods();
            Creature creature = new Creature();
            creature.Gender = GenderType.Female;
            GenderType result = methods.GetGender(creature);
            Assert.AreEqual(result, GenderType.Female);
        }

        [TestMethod]
        public void GetGender_EqualsUnknown()
        {
            ScriptMethods methods = new ScriptMethods();
            Creature creature = new Creature();
            creature.Gender = GenderType.Unknown;
            GenderType result = methods.GetGender(creature);
            Assert.AreEqual(result, GenderType.Unknown);
        }

        #endregion

        #region GetItemInSlot tests

        [TestMethod]
        public void GetItemInSlot_NullCreatureIsNull()
        {
            ScriptMethods methods = new ScriptMethods();
            Item result = methods.GetItemInSlot(null, InventorySlot.Ammo);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetItemInSlot_NoItemIsNull()
        {
            ScriptMethods methods = BuildItemScriptMethods();
            Creature creature = new Creature();
            Item result = methods.GetItemInSlot(creature, InventorySlot.Ammo);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetItemInSlot_AmmoSlotMatches()
        {
            Item item = new Item
            {
                Name = "itemName",
                Tag = "itemTag",
                Resref = "AmmoResref"
            };
            ScriptMethods methods = BuildItemScriptMethods(item);
            Creature creature = new Creature();
            creature.EquippedItemResrefs[InventorySlot.Ammo] = "AmmoResref";
            Item result = methods.GetItemInSlot(creature, InventorySlot.Ammo);

            Assert.AreEqual(result.Resref, item.Resref);
        }

        [TestMethod]
        public void GetItemInSlot_ArmsSlotMatches()
        {
            Item item = new Item
            {
                Name = "itemName",
                Tag = "itemTag",
                Resref = "ArmsResref"
            };
            ScriptMethods methods = BuildItemScriptMethods(item);
            Creature creature = new Creature();
            creature.EquippedItemResrefs[InventorySlot.Arms] = "ArmsResref";
            Item result = methods.GetItemInSlot(creature, InventorySlot.Arms);

            Assert.AreEqual(result.Resref, item.Resref);
        }


        [TestMethod]
        public void GetItemInSlot_BackSlotMatches()
        {
            Item item = new Item
            {
                Name = "itemName",
                Tag = "itemTag",
                Resref = "BackResref"
            };
            ScriptMethods methods = BuildItemScriptMethods(item);
            Creature creature = new Creature();
            creature.EquippedItemResrefs[InventorySlot.Back] = "BackResref";
            Item result = methods.GetItemInSlot(creature, InventorySlot.Back);

            Assert.AreEqual(result.Resref, item.Resref);
        }


        [TestMethod]
        public void GetItemInSlot_BodySlotMatches()
        {
            Item item = new Item
            {
                Name = "itemName",
                Tag = "itemTag",
                Resref = "BodyResref"
            };
            ScriptMethods methods = BuildItemScriptMethods(item);
            Creature creature = new Creature();
            creature.EquippedItemResrefs[InventorySlot.Body] = "BodyResref";
            Item result = methods.GetItemInSlot(creature, InventorySlot.Body);

            Assert.AreEqual(result.Resref, item.Resref);
        }



        [TestMethod]
        public void GetItemInSlot_HeadSlotMatches()
        {
            Item item = new Item
            {
                Name = "itemName",
                Tag = "itemTag",
                Resref = "HeadResref"
            };
            ScriptMethods methods = BuildItemScriptMethods(item);
            Creature creature = new Creature();
            creature.EquippedItemResrefs[InventorySlot.Head] = "HeadResref";
            Item result = methods.GetItemInSlot(creature, InventorySlot.Head);

            Assert.AreEqual(result.Resref, item.Resref);
        }


        [TestMethod]
        public void GetItemInSlot_MainHandSlotMatches()
        {
            Item item = new Item
            {
                Name = "itemName",
                Tag = "itemTag",
                Resref = "MainHandResref"
            };
            ScriptMethods methods = BuildItemScriptMethods(item);
            Creature creature = new Creature();
            creature.EquippedItemResrefs[InventorySlot.MainHand] = "MainHandResref";
            Item result = methods.GetItemInSlot(creature, InventorySlot.MainHand);

            Assert.AreEqual(result.Resref, item.Resref);
        }

        [TestMethod]
        public void GetItemInSlot_NeckSlotMatches()
        {
            Item item = new Item
            {
                Name = "itemName",
                Tag = "itemTag",
                Resref = "NeckResref"
            };
            ScriptMethods methods = BuildItemScriptMethods(item);
            Creature creature = new Creature();
            creature.EquippedItemResrefs[InventorySlot.Neck] = "NeckResref";
            Item result = methods.GetItemInSlot(creature, InventorySlot.Neck);

            Assert.AreEqual(result.Resref, item.Resref);
        }

        [TestMethod]
        public void GetItemInSlot_OffHandSlotMatches()
        {
            Item item = new Item
            {
                Name = "itemName",
                Tag = "itemTag",
                Resref = "OffHandResref"
            };
            ScriptMethods methods = BuildItemScriptMethods(item);
            Creature creature = new Creature();
            creature.EquippedItemResrefs[InventorySlot.OffHand] = "OffHandResref";
            Item result = methods.GetItemInSlot(creature, InventorySlot.OffHand);

            Assert.AreEqual(result.Resref, item.Resref);
        }

        [TestMethod]
        public void GetItemInSlot_Ring1SlotMatches()
        {
            Item item = new Item
            {
                Name = "itemName",
                Tag = "itemTag",
                Resref = "Ring1Resref"
            };
            ScriptMethods methods = BuildItemScriptMethods(item);
            Creature creature = new Creature();
            creature.EquippedItemResrefs[InventorySlot.Ring1] = "Ring1Resref";
            Item result = methods.GetItemInSlot(creature, InventorySlot.Ring1);

            Assert.AreEqual(result.Resref, item.Resref);
        }

        [TestMethod]
        public void GetItemInSlot_Ring2SlotMatches()
        {
            Item item = new Item
            {
                Name = "itemName",
                Tag = "itemTag",
                Resref = "Ring2Resref"
            };
            ScriptMethods methods = BuildItemScriptMethods(item);
            Creature creature = new Creature();
            creature.EquippedItemResrefs[InventorySlot.Ring2] = "Ring2Resref";
            Item result = methods.GetItemInSlot(creature, InventorySlot.Ring2);

            Assert.AreEqual(result.Resref, item.Resref);
        }

        [TestMethod]
        public void GetItemInSlot_WaistSlotMatches()
        {
            Item item = new Item
            {
                Name = "itemName",
                Tag = "itemTag",
                Resref = "WaistResref"
            };
            ScriptMethods methods = BuildItemScriptMethods(item);
            Creature creature = new Creature();
            creature.EquippedItemResrefs[InventorySlot.Waist] = "WaistResref";
            Item result = methods.GetItemInSlot(creature, InventorySlot.Waist);

            Assert.AreEqual(result.Resref, item.Resref);
        }

        #endregion
    }
}
