using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Entities.Contracts;

namespace Ceriyo.Core.Entities
{
    public class Ability: IGameEntity
    {
        private readonly IComponentFactory _componentFactory;

        public Ability(IComponentFactory componentFactory)
        {
            _componentFactory = componentFactory;
        }

        public void BuildEntity(Entity entity)
        {
            entity.AddComponent(_componentFactory.Create<Nameable>());
            entity.AddComponent(_componentFactory.Create<Tag>());
            entity.AddComponent(_componentFactory.Create<Resref>());
            entity.AddComponent(_componentFactory.Create<Description>());

            ScriptGroup scriptGroup = _componentFactory.Create<ScriptGroup>();
            scriptGroup.Add(ScriptEvent.OnAbilityActivated, string.Empty);

            entity.AddComponent(scriptGroup);
            
        }
    }
}
