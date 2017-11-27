using System;
using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Entities.Contracts;

namespace Ceriyo.Core.Entities
{
    /// <inheritdoc />
    public class Module: IGameEntity<ModuleData>
    {
        private readonly IComponentFactory _factory;

        /// <summary>
        /// Constructs a new module entity.
        /// </summary>
        /// <param name="factory">The component factory used for creating new components.</param>
        public Module(IComponentFactory factory)
        {
            _factory = factory;
        }

        /// <inheritdoc />
        public void BuildEntity(Entity entity, ModuleData data)
        {
            if(data == null)
                throw new ArgumentException($"{nameof(data)} cannot be null.");

            var name = _factory.Create<Nameable>();
            var tag = _factory.Create<Tag>();
            var resref = _factory.Create<Resref>();
            var description = _factory.Create<Description>();
            var scriptGroup = _factory.Create<ScriptGroup>();
            var localData = _factory.Create<LocalData>();
            var heartbeat = _factory.Create<Heartbeat>();

            name.Value = data.Name;
            tag.Value = data.Tag;
            resref.Value = data.Resref;
            description.Value = data.Description;
            heartbeat.Interval = 6.0f;

            scriptGroup.Add(ScriptEvent.OnModulePlayerEnter, data.OnPlayerEnter);
            scriptGroup.Add(ScriptEvent.OnModulePlayerLeaving, data.OnPlayerLeaving);
            scriptGroup.Add(ScriptEvent.OnModulePlayerLeft, data.OnPlayerLeft);
            scriptGroup.Add(ScriptEvent.OnHeartbeat, data.OnHeartbeat);
            scriptGroup.Add(ScriptEvent.OnModuleLoad, data.OnModuleLoad);
            scriptGroup.Add(ScriptEvent.OnPlayerDying, data.OnPlayerDying);
            scriptGroup.Add(ScriptEvent.OnPlayerDeath, data.OnPlayerDeath);
            scriptGroup.Add(ScriptEvent.OnPlayerRespawn, data.OnPlayerRespawn);
            scriptGroup.Add(ScriptEvent.OnModulePlayerLevelUp, data.OnPlayerLevelUp);
            
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
            entity.AddComponent(scriptGroup);
            entity.AddComponent(localData);
            entity.AddComponent(heartbeat);
        }
    }
}
