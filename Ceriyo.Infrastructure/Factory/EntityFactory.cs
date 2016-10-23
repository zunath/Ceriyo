using Artemis;
using Autofac;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Entities.Contracts;

namespace Ceriyo.Infrastructure.Factory
{
    public class EntityFactory: IEntityFactory
    {
        private readonly EntityWorld _world;
        private readonly IComponentContext _context;

        public EntityFactory(EntityWorld world, IComponentContext context)
        {
            _world = world;
            _context = context;
        }

        public Entity Create<T>()
            where T : IGameEntity
        {
            Entity entity = _world.CreateEntity();
            IGameEntity gameObject = _context.ResolveNamed<IGameEntity>(typeof(T).ToString());
            gameObject.BuildEntity(entity);
            return entity;
        }

        public Entity Create<TEntity, TArgument>(TArgument args) 
            where TEntity : IGameEntity<TArgument>
            where TArgument : class
        {
            Entity entity = _world.CreateEntity();
            IGameEntity<TArgument> gameObject = _context.Resolve<IGameEntity<TArgument>>();
            gameObject.BuildEntity(entity, args);
            return entity;
        }
    }
}
