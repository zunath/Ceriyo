using Ceriyo.Data.EventArguments;
using Ceriyo.Data.Packets;
using Ceriyo.Library.Global;
using Ceriyo.Network;
using FlatRedBall.Input;

namespace Ceriyo.Entities
{
    public class PlayerEntity : GraphicEntity
    {
        public PlayerEntity()
            : base("PlayerEntity")
        {

        }

        protected override void CustomInitialize()
        {
            GameGlobal.OnPacketReceived += GameGlobal_OnPacketReceived;
        }

        protected override void CustomActivity()
        {
            SendInputKeys();
        }

        protected override void CustomDestroy()
        {
            GameGlobal.OnPacketReceived -= GameGlobal_OnPacketReceived;
        }

        #region Packet Processing

        private void SendInputKeys()
        {
            PlayerInputPacket packet = new PlayerInputPacket
            {
                IsDownKeyPressed = InputManager.Keyboard.KeyDown(GameGlobal.Settings.DownKey),
                IsLeftKeyPressed = InputManager.Keyboard.KeyDown(GameGlobal.Settings.LeftKey),
                IsRightKeyPressed = InputManager.Keyboard.KeyDown(GameGlobal.Settings.RightKey),
                IsUpKeyPressed = InputManager.Keyboard.KeyDown(GameGlobal.Settings.UpKey),
                IsInventoryKeyPressed = InputManager.Keyboard.KeyDown(GameGlobal.Settings.InventoryKey),
                IsHotBar1KeyPressed = InputManager.Keyboard.KeyDown(GameGlobal.Settings.HotBar1Key),
                IsHotBar2KeyPressed = InputManager.Keyboard.KeyDown(GameGlobal.Settings.HotBar2Key),
                IsHotBar3KeyPressed = InputManager.Keyboard.KeyDown(GameGlobal.Settings.HotBar3Key),
                IsHotBar4KeyPressed = InputManager.Keyboard.KeyDown(GameGlobal.Settings.HotBar4Key),
                IsHotBar5KeyPressed = InputManager.Keyboard.KeyDown(GameGlobal.Settings.HotBar5Key),
                IsHotBar6KeyPressed = InputManager.Keyboard.KeyDown(GameGlobal.Settings.HotBar6Key),
                IsHotBar7KeyPressed = InputManager.Keyboard.KeyDown(GameGlobal.Settings.HotBar7Key),
                IsHotBar8KeyPressed = InputManager.Keyboard.KeyDown(GameGlobal.Settings.HotBar8Key),
                IsHotBar9KeyPressed = InputManager.Keyboard.KeyDown(GameGlobal.Settings.HotBar9Key),
                IsHotBar10KeyPressed = InputManager.Keyboard.KeyDown(GameGlobal.Settings.HotBar10Key)
            };

            GameGlobal.Agent.SendPacket(packet, GameGlobal.Agent.Connections[0], Lidgren.Network.NetDeliveryMethod.Unreliable);
        }

        private void GameGlobal_OnPacketReceived(object sender, PacketEventArgs e)
        {

        }

        #endregion
    }
}
