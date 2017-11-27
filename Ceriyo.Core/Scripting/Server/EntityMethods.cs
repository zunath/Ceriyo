using System;
using Artemis;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Components;
using Ceriyo.Core.Scripting.Server.Contracts;
using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Core.Scripting.Server
{
    /// <inheritdoc />
    [ScriptNamespace("Entity")]
    public class EntityMethods : IEntityMethods
    {
        private readonly IEngineService _engineService;

        /// <inheritdoc />
        public EntityMethods(IEngineService engineService)
        {
            _engineService = engineService;
        }

        /// <inheritdoc />
        public string GetName(Entity entity)
        {
            try
            {
                return entity.GetComponent<Nameable>().Value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <inheritdoc />
        public void SetName(Entity entity, string value)
        {
            try
            {
                if (!entity.HasComponent<Nameable>()) return;
                if (value == null) value = string.Empty;
                int maxLength = _engineService.MaxNameLength;

                if (value.Length > maxLength)
                    value = value.Substring(0, maxLength);

                Nameable component = entity.GetComponent<Nameable>();
                component.Value = value;
            }
            catch (Exception)
            {
                // Do nothing.
            }
        }


        /// <inheritdoc />
        public string GetTag(Entity entity)
        {
            try
            {
                return entity.GetComponent<Tag>().Value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <inheritdoc />
        public void SetTag(Entity entity, string value)
        {
            try
            {
                if (!entity.HasComponent<Tag>()) return;
                if (value == null) value = string.Empty;
                int maxLength = _engineService.MaxTagLength;

                if (value.Length > maxLength)
                    value = value.Substring(0, maxLength);

                Tag component = entity.GetComponent<Tag>();
                component.Value = value;
            }
            catch (Exception)
            {
                // Do nothing.
            }
        }

        /// <inheritdoc />
        public string GetResref(Entity entity)
        {
            try
            {
                return entity.GetComponent<Resref>().Value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
