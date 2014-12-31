using Microsoft.Xna.Framework.Input;

namespace Ceriyo.Data.Settings
{
    public class GameSettings
    {
        public int Port { get; set; }

        public Keys InventoryKey { get; set; }
        public Keys UpKey { get; set; }
        public Keys DownKey { get; set; }
        public Keys RightKey { get; set; }
        public Keys LeftKey { get; set; }
        public Keys HotBar1Key { get; set; }
        public Keys HotBar2Key { get; set; }
        public Keys HotBar3Key { get; set; }
        public Keys HotBar4Key { get; set; }
        public Keys HotBar5Key { get; set; }
        public Keys HotBar6Key { get; set; }
        public Keys HotBar7Key { get; set; }
        public Keys HotBar8Key { get; set; }
        public Keys HotBar9Key { get; set; }
        public Keys HotBar10Key { get; set; }

        public GameSettings()
        {
            this.Port = 5121;
            InventoryKey = Keys.I;
            UpKey = Keys.W;
            DownKey = Keys.S;
            RightKey = Keys.D;
            LeftKey = Keys.A;
            HotBar1Key = Keys.D1;
            HotBar2Key = Keys.D2;
            HotBar3Key = Keys.D3;
            HotBar4Key = Keys.D4;
            HotBar5Key = Keys.D5;
            HotBar6Key = Keys.D6;
            HotBar7Key = Keys.D7;
            HotBar8Key = Keys.D8;
            HotBar9Key = Keys.D9;
            HotBar10Key = Keys.D0;
        }
    }
}
