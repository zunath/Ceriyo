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
        public int GetAttributeLevel(Creature creature, AttributeTypeEnum type)
        {
            try
            {
                switch (type)
                {
                    case AttributeTypeEnum.Charisma: return creature.Charisma;
                    case AttributeTypeEnum.Constitution: return creature.Constitution;
                    case AttributeTypeEnum.Dexterity: return creature.Dexterity;
                    case AttributeTypeEnum.Intelligence: return creature.Intelligence;
                    case AttributeTypeEnum.Strength: return creature.Strength;
                    case AttributeTypeEnum.Wisdom: return creature.Wisdom;
                    default: return -1;
                }
            }
            catch
            {
                return -1;
            }
        }

        [ScriptMethod]
        public GenderTypeEnum GetGender(Creature creature)
        {
            try
            {
                return creature.Gender;
            }
            catch
            {
                return GenderTypeEnum.Unknown;
            }
        }



    }
}
