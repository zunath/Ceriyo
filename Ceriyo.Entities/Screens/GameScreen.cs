using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;
using Ceriyo.Entities.DrawableBatches;
using Ceriyo.Library.Network;
using Ceriyo.Library.Network.Packets;
using FlatRedBall;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace Ceriyo.Entities.Screens
{
    public class GameScreen : BaseScreen
    {
        private Area _area;
        private GameResource _tilesetGraphicResource;
        private MapDrawableBatch _map;

        public GameScreen()
            : base("GameScreen")
        {
        }

        protected override void CustomInitialize()
        {
            SubscribePacketActions();
            new GameScreenInitPacket { IsRequest = true }.Send(NetDeliveryMethod.ReliableUnordered);
            SpriteManager.Camera.BackgroundColor = Color.LightGray;
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
        }

        protected override void CustomDestroy()
        {
            UnsubscribePacketActions();
            _map.Destroy();
        }

        private void SubscribePacketActions()
        {
            NetworkManager.SubscribePacketAction(typeof(GameScreenInitPacket), ReceiveGameScreenInitPacket);
        }

        private void UnsubscribePacketActions()
        {
            NetworkManager.UnsubscribePacketAction(typeof(GameScreenInitPacket), ReceiveGameScreenInitPacket);
        }

        private void ReceiveGameScreenInitPacket(PacketBase packetBase)
        {
            GameScreenInitPacket packet = (GameScreenInitPacket)packetBase;
            _area = new Area
            {
                Description = packet.AreaDescription,
                LayerCount = packet.AreaLayers,
                Name = packet.AreaName,
                MapTiles = packet.AreaTiles,
                Resref = packet.AreaResref,
                Tag = packet.AreaTag
            };

            _tilesetGraphicResource = new GameResource
                (packet.TilesetGraphicResourcePackage, 
                packet.TilesetGraphicResourceFileName, 
                ResourceTypeEnum.Graphic);

            _map = new MapDrawableBatch(_area, _tilesetGraphicResource);
            SpriteManager.AddDrawableBatch(_map);
        }
    }
}
