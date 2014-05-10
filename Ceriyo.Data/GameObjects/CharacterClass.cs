using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.GameObjects
{
    public class CharacterClass : IGameObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string WorkingDirectory { get { return WorkingPaths.CharacterClassesDirectory; } }
    }
}
