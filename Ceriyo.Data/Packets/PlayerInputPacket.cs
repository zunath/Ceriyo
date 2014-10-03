using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class PlayerInputPacket : PacketBase
    {
        [ProtoMember(1)]
        public bool IsInventoryKeyPressed { get; set; }
        [ProtoMember(2)]
        public bool IsUpKeyPressed { get; set; }
        [ProtoMember(3)]
        public bool IsDownKeyPressed { get; set; }
        [ProtoMember(4)]
        public bool IsRightKeyPressed { get; set; }
        [ProtoMember(5)]
        public bool IsLeftKeyPressed { get; set; }
        [ProtoMember(6)]
        public bool IsHotBar1KeyPressed { get; set; }
        [ProtoMember(7)]
        public bool IsHotBar2KeyPressed { get; set; }
        [ProtoMember(8)]
        public bool IsHotBar3KeyPressed { get; set; }
        [ProtoMember(9)]
        public bool IsHotBar4KeyPressed { get; set; }
        [ProtoMember(10)]
        public bool IsHotBar5KeyPressed { get; set; }
        [ProtoMember(11)]
        public bool IsHotBar6KeyPressed { get; set; }
        [ProtoMember(12)]
        public bool IsHotBar7KeyPressed { get; set; }
        [ProtoMember(13)]
        public bool IsHotBar8KeyPressed { get; set; }
        [ProtoMember(14)]
        public bool IsHotBar9KeyPressed { get; set; }
        [ProtoMember(15)]
        public bool IsHotBar10KeyPressed { get; set; }

        public PlayerInputPacket()
        {
            this.IsInventoryKeyPressed = false;
            this.IsUpKeyPressed = false;
            this.IsDownKeyPressed = false;
            this.IsRightKeyPressed = false;
            this.IsLeftKeyPressed = false;
            this.IsHotBar1KeyPressed = false;
            this.IsHotBar2KeyPressed = false;
            this.IsHotBar3KeyPressed = false;
            this.IsHotBar4KeyPressed = false;
            this.IsHotBar5KeyPressed = false;
            this.IsHotBar6KeyPressed = false;
            this.IsHotBar7KeyPressed = false;
            this.IsHotBar8KeyPressed = false;
            this.IsHotBar9KeyPressed = false;
            this.IsHotBar10KeyPressed = false;
        }
    }
}
