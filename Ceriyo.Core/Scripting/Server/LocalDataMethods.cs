using Artemis;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Components;
using Ceriyo.Core.Scripting.Server.Contracts;

namespace Ceriyo.Core.Scripting.Server
{
    [ScriptNamespace("LocalData")]
    public class LocalDataMethods: ILocalDataMethods
    {
        public void SetLocalValue(Entity entity, string key, string value)
        {
            if (!entity.HasComponent<LocalData>()) return;
            LocalData data = entity.GetComponent<LocalData>();
            data.LocalStrings[key] = value;
        }

        public void SetLocalValue(Entity entity, string key, double value)
        {
            if (!entity.HasComponent<LocalData>()) return;
            LocalData data = entity.GetComponent<LocalData>();
            data.LocalDoubles[key] = value;
        }

        public string GetLocalString(Entity entity, string key)
        {
            if (!entity.HasComponent<LocalData>()) return string.Empty;
            LocalData data = entity.GetComponent<LocalData>();
            return data.LocalStrings.ContainsKey(key) ? data.LocalStrings[key] : string.Empty;
        }

        public double GetLocalNumber(Entity entity, string key)
        {
            if (!entity.HasComponent<LocalData>()) return 0.0;
            LocalData data = entity.GetComponent<LocalData>();
            return data.LocalDoubles.ContainsKey(key) ? data.LocalDoubles[key] : 0.0;
        }

        public void DeleteLocalString(Entity entity, string key)
        {
            if (!entity.HasComponent<LocalData>()) return;
            LocalData data = entity.GetComponent<LocalData>();
            if (data.LocalStrings.ContainsKey(key))
                data.LocalStrings.Remove(key);
        }
        public void DeleteLocalNumber(Entity entity, string key)
        {
            if (!entity.HasComponent<LocalData>()) return;
            LocalData data = entity.GetComponent<LocalData>();
            if (data.LocalDoubles.ContainsKey(key))
                data.LocalDoubles.Remove(key);
        }
    }
}
