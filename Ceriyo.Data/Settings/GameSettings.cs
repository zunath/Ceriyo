using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.Settings
{
    public class GameSettings
    {
        public int Port { get; set; }

        public GameSettings()
        {
            this.Port = 5121;
        }
    }
}
