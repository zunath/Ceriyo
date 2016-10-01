using System;
using Ceriyo.Core.Data;
using ProtoBuf.Meta;

namespace Ceriyo.Infrastructure.Serialization
{
    public class ProtobufContext
    {
        public static void Build()
        {
            Map<AbilityData>();
            Map<AnimationData>();
            Map<ClassData>();
            Map<ClassRequirementData>();
            Map<CreatureData>();
            Map<DialogData>();
            Map<FrameData>();
            Map<ItemData>();
            Map<ItemPropertyData>();
            Map<ItemTypeData>();
            Map<LocalVariableData>();
            Map<ModuleData>();
            Map<ModuleFileData>();
            Map<PlaceableData>();
            Map<ScriptData>();
            Map<SkillData>();
            Map<TilesetData>();
        }

        private static void Map<T>()
            where T: class
        {
            Type type = typeof(T);
            var registeredTypes = RuntimeTypeModel.Default.GetTypes();

            // Don't register the same type twice.
            foreach (MetaType registeredType in registeredTypes)
            {
                if (registeredType.Type == type) return;
            }
            

            var meta = RuntimeTypeModel.Default.Add(type, false);
            var properties = type.GetProperties();

            for (int x = 0; x < properties.Length; x++)
            {
                var prop = properties[x];
                
                meta.Add(x+1, prop.Name);
            }

        }

    }
}
