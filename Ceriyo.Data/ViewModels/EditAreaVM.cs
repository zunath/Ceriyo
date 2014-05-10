using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class EditAreaVM : BaseVM
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        IList<string> Scripts { get; set; }
        IList<LocalVariable> LocalVariables { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string OnAreaEnterScript { get; set; }
        public string OnAreaExitScript { get; set; }
        public string OnAreaHeartbeatScript { get; set; }
        

        public EditAreaVM()
        {
        }
    }
}
