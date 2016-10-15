﻿using System.Collections.Generic;
using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    public class LocalData: IComponent
    {
        public Dictionary<string, string> LocalStrings { get; set; }
        public Dictionary<string, double> LocalDoubles { get; set; }

        public LocalData()
        {
            LocalStrings = new Dictionary<string, string>();
            LocalDoubles = new Dictionary<string, double>();
        }
    }
}
