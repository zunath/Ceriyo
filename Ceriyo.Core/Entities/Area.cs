using System;
using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Entities.Contracts;

namespace Ceriyo.Core.Entities
{
    public class Area: IGameEntity<AreaData>
    {
        private readonly IComponentFactory _factory;
        public Area(IComponentFactory factory)
        {
            _factory = factory;
        }

        public void BuildEntity(Entity entity, AreaData data) 
        {
            if(data == null)
                throw new ArgumentException($"{nameof(data)} cannot be null.");

            var name = _factory.Create<Nameable>();
            var tag = _factory.Create<Tag>();
            var resref = _factory.Create<Resref>();
            var description = _factory.Create<Description>();
            var onAreaEnter = _factory.Create<Script>();
            var onAreaExit = _factory.Create<Script>();
            var onAreaHeartbeat = _factory.Create<Script>();
            var localData = _factory.Create<LocalData>();

            name.Value = data.Name;
            tag.Value = data.Tag;
            resref.Value = data.Resref;
            description.Value = data.Description;

            onAreaEnter.Name = data.OnAreaEnter;
            onAreaEnter.Event = ScriptEvent.OnAreaEnter;

            onAreaExit.Name = data.OnAreaExit;
            onAreaExit.Event = ScriptEvent.OnAreaExit;

            onAreaHeartbeat.Name = data.OnAreaHeartbeat;
            onAreaHeartbeat.Event = ScriptEvent.OnAreaHeartbeat;

            foreach (var @string in data.LocalVariables.LocalStrings)
            {
                localData.LocalStrings.Add(@string.Key, @string.Value);
            }

            foreach (var @double in data.LocalVariables.LocalDoubles)
            {
                localData.LocalDoubles.Add(@double.Key, @double.Value);
            }

            entity.AddComponent(name);
            entity.AddComponent(tag);
            entity.AddComponent(resref);
            entity.AddComponent(description);
            entity.AddComponent(onAreaEnter);
            entity.AddComponent(onAreaExit);
            entity.AddComponent(onAreaHeartbeat);
            entity.AddComponent(localData);
        }
    }
}
