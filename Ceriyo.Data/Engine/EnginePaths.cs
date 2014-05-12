﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall.IO;

namespace Ceriyo.Data
{
    public static class EnginePaths
    {
        public static string DataExtension
        {
            get { return ".xml"; }
        }

        public static string ScriptExtension
        {
            get { return ".js"; }
        }

        public static string BackupExtension
        {
            get { return ".bak"; }
        }

        public static string ModuleDataFileName
        {
            get { return "Module"; }
        }

        public static string ResourcePackDataFileName
        {
            get { return "Manifest"; }
        }

        public static string ModulesDirectory
        {
            get { return FileManager.RelativeDirectory + @"Content/Modules/"; }
        }

        public static string CharactersDirectory
        {
            get { return FileManager.RelativeDirectory + @"Content/Characters/"; }
        }

        public static string ScriptsDirectory
        {
            get { return FileManager.RelativeDirectory + @"Content/Scripts/"; }
        }

        public static string WorkingDirectory
        {
            get { return FileManager.RelativeDirectory + @"Content/temp/"; }
        }

        public static string LogsDirectory
        {
            get { return FileManager.RelativeDirectory + @"Content/logs/"; }
        }

        public static string SettingsDirectory
        {
            get { return FileManager.RelativeDirectory + @"Content/Settings/"; }
        }

        public static string DataDirectory
        {
            get { return FileManager.RelativeDirectory + @"Content/Data/"; }
        }
    }
}
