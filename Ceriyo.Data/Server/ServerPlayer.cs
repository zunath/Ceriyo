using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.Server
{
    public class ServerPlayer
    {
        public string Username { get; set; }
        public Player PC { get; set; }

        public ServerPlayer()
        {
            this.Username = string.Empty;
            this.PC = new Player();
        }
    }
}
