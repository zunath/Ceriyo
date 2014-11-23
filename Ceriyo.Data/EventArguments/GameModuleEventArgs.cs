using System;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.EventArguments
{
    public class GameModuleEventArgs : EventArgs
    {
        public string FileName { get; set; }

        public GameModuleEventArgs(string fileName)
        {
            FileName = fileName;
        }
    }
}
