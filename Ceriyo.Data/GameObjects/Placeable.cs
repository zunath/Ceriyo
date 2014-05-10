﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.Enumerations;

namespace Ceriyo.Data.GameObjects
{
    public class Placeable : IGameObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string WorkingDirectory { get { return WorkingPaths.PlaceablesDirectory; } }
        public IList<LocalVariable> LocalVariables { get; set; }
        public IDictionary<ScriptEventTypeEnum, string> Scripts { get; set; }
    }
}
