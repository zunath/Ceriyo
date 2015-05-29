using System;

namespace Ceriyo.Library.ScriptEngine
{
    public partial class ScriptMethods
    {
        [ScriptMethod]
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
        
        [ScriptMethod]
        public int GetExperienceRequiredForLevel(int level)
        {
            try
            {
                return _levelChart.Levels[level - 1].ExperienceRequired;
            }
            catch
            {
                return -1;
            }
        }

    }
}
