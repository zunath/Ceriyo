using Ceriyo.Library.Global;
using Ceriyo.Library.Network;
using Ceriyo.Library.Network.Packets;
using FlatRedBall.Input;
using Lidgren.Network;

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
            CeriyoServices.OnPacketReceived += GameGlobal_OnPacketReceived;
        }

        protected override void CustomActivity()
        {
            SendInputKeys();
        }

        protected override void CustomDestroy()
        {
            CeriyoServices.OnPacketReceived -= GameGlobal_OnPacketReceived;
        }

        #region Packet Processing

        private void SendInputKeys()
        {
            // TODO: Figure out where settings are going to be located and put them in this packet.
            PlayerInputPacket packet = new PlayerInputPacket
            {
                //IsDownKeyPressed = InputManager.Keyboard.KeyDown(CeriyoServices.Settings.DownKey),
                //IsLeftKeyPressed = InputManager.Keyboard.KeyDown(CeriyoServices.Settings.LeftKey),
                //IsRightKeyPressed = InputManager.Keyboard.KeyDown(CeriyoServices.Settings.RightKey),
                //IsUpKeyPressed = InputManager.Keyboard.KeyDown(CeriyoServices.Settings.UpKey),
                //IsInventoryKeyPressed = InputManager.Keyboard.KeyDown(CeriyoServices.Settings.InventoryKey),
                //IsHotBar1KeyPressed = InputManager.Keyboard.KeyDown(CeriyoServices.Settings.HotBar1Key),
                //IsHotBar2KeyPressed = InputManager.Keyboard.KeyDown(CeriyoServices.Settings.HotBar2Key),
                //IsHotBar3KeyPressed = InputManager.Keyboard.KeyDown(CeriyoServices.Settings.HotBar3Key),
                //IsHotBar4KeyPressed = InputManager.Keyboard.KeyDown(CeriyoServices.Settings.HotBar4Key),
                //IsHotBar5KeyPressed = InputManager.Keyboard.KeyDown(CeriyoServices.Settings.HotBar5Key),
                //IsHotBar6KeyPressed = InputManager.Keyboard.KeyDown(CeriyoServices.Settings.HotBar6Key),
                //IsHotBar7KeyPressed = InputManager.Keyboard.KeyDown(CeriyoServices.Settings.HotBar7Key),
                //IsHotBar8KeyPressed = InputManager.Keyboard.KeyDown(CeriyoServices.Settings.HotBar8Key),
                //IsHotBar9KeyPressed = InputManager.Keyboard.KeyDown(CeriyoServices.Settings.HotBar9Key),
                //IsHotBar10KeyPressed = InputManager.Keyboard.KeyDown(CeriyoServices.Settings.HotBar10Key)
            };

            packet.Send(NetDeliveryMethod.Unreliable);
        }

        private void GameGlobal_OnPacketReceived(object sender, PacketEventArgs e)
        {

        }

        #endregion
    }
}
