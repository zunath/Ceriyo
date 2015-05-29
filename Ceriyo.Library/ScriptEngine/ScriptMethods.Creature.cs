using System.Linq;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Library.ScriptEngine
{
    public partial class ScriptMethods
    {
        [ScriptMethod]
        public int GetLevel(Creature creature)
        {
            try
            {
                return creature.Level;
            }
            catch
            {
                return -1;
            }
        }

        [ScriptMethod]
        public int GetCurrentHitPoints(Creature creature)
        {
            try
            {
                return creature.HitPoints;
            }
            catch
            {
                return -1;
            }
        }

        [ScriptMethod]
        public int GetCurrentMana(Creature creature)
        {
            try
            {
                return creature.Mana;
            }
            catch
            {
                return -1;
            }
        }

        [ScriptMethod]
        public int GetAttributeLevel(Creature creature, AttributeType type)
        {
            try
            {
                switch (type)
                {
                    case AttributeType.Charisma: return creature.Charisma;
                    case AttributeType.Constitution: return creature.Constitution;
                    case AttributeType.Dexterity: return creature.Dexterity;
                    case AttributeType.Intelligence: return creature.Intelligence;
                    case AttributeType.Strength: return creature.Strength;
                    case AttributeType.Wisdom: return creature.Wisdom;
                    default: return -1;
                }
            }
            catch
            {
                return -1;
            }
        }

        [ScriptMethod]
        public GenderType GetGender(Creature creature)
        {
            try
            {
                return creature.Gender;
            }
            catch
            {
                return GenderType.Unknown;
            }
        }

        [ScriptMethod]
        public Item GetItemInSlot(Creature creature, InventorySlot slot)
        {
            try
            {
                string resref = creature.EquippedItemResrefs[slot];
                return _items.SingleOrDefault(x => x.Resref == resref);
            }
            catch
            {
                return null;
            }
        }

    }
}
