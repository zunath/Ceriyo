using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Contracts;

namespace Ceriyo.Core.Scripting.Server
{
    public class LocalDataMethods: IServerScriptMethodGroup
    {
        public static void SetLocalValue(Entity entity, string key, string value)
        {
            if (!entity.HasComponent<LocalData>()) return;
            LocalData data = entity.GetComponent<LocalData>();
            data.LocalStrings[key] = value;
        }

        public static void SetLocalValue(Entity entity, string key, float value)
        {
            if (!entity.HasComponent<LocalData>()) return;
            LocalData data = entity.GetComponent<LocalData>();
            data.LocalFloats[key] = value;
        }

        public static string GetLocalString(Entity entity, string key)
        {
            if (!entity.HasComponent<LocalData>()) return string.Empty;
            LocalData data = entity.GetComponent<LocalData>();
            return data.LocalStrings.ContainsKey(key) ? data.LocalStrings[key] : string.Empty;
        }

        public static float GetLocalNumber(Entity entity, string key)
        {
            if (!entity.HasComponent<LocalData>()) return 0.0f;
            LocalData data = entity.GetComponent<LocalData>();
            return data.LocalFloats.ContainsKey(key) ? data.LocalFloats[key] : 0.0f;
        }

        public static void DeleteLocalString(Entity entity, string key)
        {
            if (!entity.HasComponent<LocalData>()) return;
            LocalData data = entity.GetComponent<LocalData>();
            if (data.LocalStrings.ContainsKey(key))
                data.LocalStrings.Remove(key);
        }
        public static void DeleteLocalNumber(Entity entity, string key)
        {
            if (!entity.HasComponent<LocalData>()) return;
            LocalData data = entity.GetComponent<LocalData>();
            if (data.LocalFloats.ContainsKey(key))
                data.LocalFloats.Remove(key);
        }
    }
}
