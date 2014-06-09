using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.GameObjects
{
    public class ItemRequirement
    {
        public CharacterClass CharacterClass { get; set; }
        public int LevelRequired { get; set; }
        public bool IsAvailable { get; set; }

        public ItemRequirement()
        {
            this.CharacterClass = new CharacterClass();
            this.LevelRequired = EngineConstants.MaxLevel;
            this.IsAvailable = true;
        }
    
    }
}
