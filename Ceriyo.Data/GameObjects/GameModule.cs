using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.GameObjects
{
    public class GameModule : IGameObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string WorkingDirectory { get { throw new NotSupportedException(); } }

        public GameModule()
        { 
        }

        public GameModule(string name, string tag, string resref)
        {
            this.Name = name;
            this.Tag = tag;
            this.Resref = resref;
        }

        public string ListBoxName
        {
            get
            {
                return Name + " (" + Resref + ModulePaths.ModuleExtension + ")";
            }
        }
    }
}
