using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.Engine;

namespace Ceriyo.Data.GameObjects
{
    public class LevelChartItem
    {
        private int _level;

        public int Level 
        {
            get
            {
                if (_level < 1) _level = 1;
                else if (_level > EngineConstants.MaxLevel) _level = EngineConstants.MaxLevel;

                return _level;
            }
            set
            {
                if (value < 1) value = 1;
                else if (value > EngineConstants.MaxLevel) value = EngineConstants.MaxLevel;

                _level = value;
            }
        }

        public int ExperienceRequired { get; set; }
        public int SkillPoints { get; set; }
        public int NewAbilities { get; set; }

        public LevelChartItem()
        {
            this.Level = 1;
            this.ExperienceRequired = 0;
            this.SkillPoints = 0;
            this.NewAbilities = 0;
        }

        public LevelChartItem(int level, int exp, int skillPoints, int newAbilities)
        {
            Level = level;
            ExperienceRequired = exp;
            SkillPoints = skillPoints;
            NewAbilities = newAbilities;
        }
    }
}
