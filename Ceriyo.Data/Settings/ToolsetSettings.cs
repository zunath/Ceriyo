﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.Settings
{
    public class ToolsetSettings
    {
        public int DataEditorHeight { get; set; }
        public int DataEditorWidth { get; set; }

        public ToolsetSettings()
        {
            DataEditorHeight = 600;
            DataEditorWidth = 700;
        }
    }
}
