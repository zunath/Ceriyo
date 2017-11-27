using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Entities.Contracts;
using Ceriyo.Core.Services.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Core.Entities
{
    /// <inheritdoc />
    public class ObjectPainter: IGameEntity<Texture2D>
    {
        private readonly IComponentFactory _factory;
        private readonly IEngineService _engineService;

        /// <summary>
        /// Constructs a new Object Painter entity.
        /// </summary>
        /// <param name="factory">The component factory used for creating new components.</param>
        /// <param name="engineService">The engine service which contains necessary constants.</param>
        public ObjectPainter(IComponentFactory factory,
            IEngineService engineService)
        {
            _factory = factory;
            _engineService = engineService;
        }

        /// <inheritdoc />
        public void BuildEntity(Entity entity, Texture2D texture)
        {
            var renderable = _factory.Create<Renderable>();
            var position = _factory.Create<Position>();
            var paintable = _factory.Create<Paintable>();

            renderable.Texture = texture;
            renderable.Source = new Rectangle(
                0, 
                0, 
                _engineService.TileWidth, 
                _engineService.TileHeight);
            //renderable.Origin = new Vector2(
            //    _engineService.TileWidth / 2.0f,
            //    _engineService.TileHeight / 2.0f);

            entity.AddComponent(renderable);
            entity.AddComponent(position);
            entity.AddComponent(paintable);
        }
    }
}
