using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Library.Network;

namespace Ceriyo.Library.Global
{
    public static class GameGlobal
    {
        public static NetworkAgent Agent { get; set; }
        public static string Username { get; set; }

        static GameGlobal()
        {
            if (EngineConstants.IsDebugEnabled)
            {
                Username = "zunath";
            }
        }
    }
}
