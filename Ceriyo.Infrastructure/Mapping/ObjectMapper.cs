using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;

namespace Ceriyo.Infrastructure.Mapping
{
    public class ObjectMapper: IObjectMapper
    {
        public void Initialize()
        {
            AutoMapper.Mapper.Initialize(c =>
            {
                c.CreateMap<ModuleData, ModuleData>();
            });
        }

        public TDestination Map<TDestination>(object source)
        {
            return AutoMapper.Mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
