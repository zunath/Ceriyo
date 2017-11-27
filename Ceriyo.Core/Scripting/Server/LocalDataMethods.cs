using Artemis;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Components;
using Ceriyo.Core.Scripting.Server.Contracts;

namespace Ceriyo.Core.Scripting.Server
{
    /// <inheritdoc />
    [ScriptNamespace("LocalData")]
    public class LocalDataMethods: ILocalDataMethods
    {
        /// <inheritdoc />
        public void SetLocalValue(Entity entity, string key, string value)
        {
            if (!entity.HasComponent<LocalData>()) return;
            LocalData data = entity.GetComponent<LocalData>();
            data.LocalStrings[key] = value;
        }

        /// <inheritdoc />
        public void SetLocalValue(Entity entity, string key, double value)
        {
            if (!entity.HasComponent<LocalData>()) return;
            LocalData data = entity.GetComponent<LocalData>();
            data.LocalDoubles[key] = value;
        }

        /// <inheritdoc />
        public string GetLocalString(Entity entity, string key)
        {
            if (!entity.HasComponent<LocalData>()) return string.Empty;
            LocalData data = entity.GetComponent<LocalData>();
            return data.LocalStrings.ContainsKey(key) ? data.LocalStrings[key] : string.Empty;
        }

        /// <inheritdoc />
        public double GetLocalNumber(Entity entity, string key)
        {
            if (!entity.HasComponent<LocalData>()) return 0.0;
            LocalData data = entity.GetComponent<LocalData>();
            return data.LocalDoubles.ContainsKey(key) ? data.LocalDoubles[key] : 0.0;
        }

        /// <inheritdoc />
        public void DeleteLocalString(Entity entity, string key)
        {
            if (!entity.HasComponent<LocalData>()) return;
            LocalData data = entity.GetComponent<LocalData>();
            if (data.LocalStrings.ContainsKey(key))
                data.LocalStrings.Remove(key);
        }

        /// <inheritdoc />
        public void DeleteLocalNumber(Entity entity, string key)
        {
            if (!entity.HasComponent<LocalData>()) return;
            LocalData data = entity.GetComponent<LocalData>();
            if (data.LocalDoubles.ContainsKey(key))
                data.LocalDoubles.Remove(key);
        }
    }
}
