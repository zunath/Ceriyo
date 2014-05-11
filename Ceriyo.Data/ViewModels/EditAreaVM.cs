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

        public IList<string> Scripts { get; set; }
        public IList<LocalVariable> LocalVariables { get; set; }
        public IList<Tileset> Tilesets { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string OnAreaEnterScript { get; set; }
        public string OnAreaExitScript { get; set; }
        public string OnAreaHeartbeatScript { get; set; }
        

        public EditAreaVM()
        {
            this.Name = "";
            this.Tag = "";
            this.Resref = "";
            this.Width = EngineConstants.AreaMinWidth;
            this.Height = EngineConstants.AreaMinHeight;
            this.Scripts = new List<string>();
            this.LocalVariables = new List<LocalVariable>();
            this.Tilesets = new List<Tileset>();
            this.Description = "";
            this.Comments = "";
            this.OnAreaEnterScript = "";
            this.OnAreaExitScript = "";
            this.OnAreaHeartbeatScript = "";
        }
    }
}
