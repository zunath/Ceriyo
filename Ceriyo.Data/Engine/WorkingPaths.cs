using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data
{
    public static class WorkingPaths
    {
        public static string AreasDirectory
        {
            get { return EnginePaths.WorkingDirectory + ModulePaths.AreasDirectory; }
        }

        public static string CharacterClassesDirectory
        {
            get { return EnginePaths.WorkingDirectory + ModulePaths.CharacterClassesDirectory; }
        }

        public static string ConversationsDirectory
        {
            get { return EnginePaths.WorkingDirectory + ModulePaths.ConversationsDirectory; }
        }

        public static string CreaturesDirectory
        {
            get { return EnginePaths.WorkingDirectory + ModulePaths.CreaturesDirectory; }
        }

        public static string ItemsDirectory
        {
            get { return EnginePaths.WorkingDirectory + ModulePaths.ItemsDirectory; }
        }

        public static string PlaceablesDirectory
        {
            get { return EnginePaths.WorkingDirectory + ModulePaths.PlaceablesDirectory; }
        }

        public static string ScriptsDirectory
        {
            get { return EnginePaths.WorkingDirectory + ModulePaths.ScriptsDirectory; }
        }

        public static string TilesetsDirectory
        {
            get { return EnginePaths.WorkingDirectory + ModulePaths.TilesetsDirectory; }
        }
    }
}
