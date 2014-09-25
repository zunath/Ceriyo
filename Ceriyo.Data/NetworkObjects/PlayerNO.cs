using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.NetworkObjects
{
    [ProtoContract]
    public class PlayerNO
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string Description { get; set; }

        public PlayerNO()
        {
            this.Name = string.Empty;
            this.Description = string.Empty;
        }
    }
}
