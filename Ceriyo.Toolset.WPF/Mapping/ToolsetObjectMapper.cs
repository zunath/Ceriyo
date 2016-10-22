using AutoMapper;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.Observables;
using IObjectMapper = Ceriyo.Core.Contracts.IObjectMapper;

namespace Ceriyo.Toolset.WPF.Mapping
{
    public class ToolsetObjectMapper: IObjectMapper
    {
        public void Initialize()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<ModuleData, ModuleData>();

                c.CreateMap<AbilityData, AbilityDataObservable>();
                c.CreateMap<AbilityDataObservable, AbilityData>();

                c.CreateMap<AnimationData, AnimationDataObservable>();
                c.CreateMap<AnimationDataObservable, AnimationData>();

                c.CreateMap<AreaData, AreaDataObservable>();
                c.CreateMap<AreaDataObservable, AreaData>();

                c.CreateMap<ClassData, ClassDataObservable>();
                c.CreateMap<ClassDataObservable, ClassData>();

                c.CreateMap<ClassLevelData, ClassLevelDataObservable>();
                c.CreateMap<ClassLevelDataObservable, ClassLevelData>();

                c.CreateMap<ClassRequirementData, ClassRequirementDataObservable>();
                c.CreateMap<ClassRequirementDataObservable, ClassRequirementData>();

                c.CreateMap<CreatureData, CreatureDataObservable>();
                c.CreateMap<CreatureDataObservable, CreatureData>();

                c.CreateMap<DialogData, DialogDataObservable>();
                c.CreateMap<DialogDataObservable, DialogData>();

                c.CreateMap<FrameData, FrameDataObservable>();
                c.CreateMap<FrameDataObservable, FrameData>();

                c.CreateMap<ItemData, ItemDataObservable>();
                c.CreateMap<ItemDataObservable, ItemData>();

                c.CreateMap<ItemPropertyData, ItemPropertyDataObservable>();
                c.CreateMap<ItemPropertyDataObservable, ItemPropertyData>();

                c.CreateMap<ItemTypeData, ItemTypeDataObservable>();
                c.CreateMap<ItemTypeDataObservable, ItemTypeData>();

                c.CreateMap<LevelChartData, LevelChartDataObservable>();
                c.CreateMap<LevelChartDataObservable, LevelChartData>();

                c.CreateMap<LocalDoubleData, LocalDoubleDataObservable>();
                c.CreateMap<LocalDoubleDataObservable, LocalDoubleData>();

                c.CreateMap<LocalStringData, LocalStringDataObservable>();

                c.CreateMap<LocalStringDataObservable, LocalStringData>();

                c.CreateMap<LocalVariableData, LocalVariableDataObservable>();
                c.CreateMap<LocalVariableDataObservable, LocalVariableData>();

                c.CreateMap<ModuleData, ModuleDataObservable>();
                c.CreateMap<ModuleDataObservable, ModuleData>();

                c.CreateMap<PlaceableData, PlaceableDataObservable>();
                c.CreateMap<PlaceableDataObservable, PlaceableData>();

                c.CreateMap<ScriptData, ScriptDataObservable>();
                c.CreateMap<ScriptDataObservable, ScriptData>();

                c.CreateMap<SkillData, SkillDataObservable>();
                c.CreateMap<SkillDataObservable, SkillData>();

                c.CreateMap<TilesetData, TilesetDataObservable>();
                c.CreateMap<TilesetDataObservable, TilesetData>();
            });
        }

        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
