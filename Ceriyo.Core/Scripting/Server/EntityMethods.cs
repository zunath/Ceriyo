using System;
using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Scripting.Server.Contracts;
using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Core.Scripting.Server
{
    public class EntityMethods : IEntityMethods
    {
        private readonly IEngineService _engineService;

        public EntityMethods(IEngineService engineService)
        {
            _engineService = engineService;
        }

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
