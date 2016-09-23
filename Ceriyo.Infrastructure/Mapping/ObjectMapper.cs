using Ceriyo.Core.Contracts;

namespace Ceriyo.Infrastructure.Mapping
{
    public class ObjectMapper: IObjectMapper
    {
        public void Initialize()
        {
            AutoMapper.Mapper.Initialize(c =>
            {
                // Mappings go here.
            });
        }

        public TDestination Map<TDestination>(object source)
        {
            return AutoMapper.Mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return AutoMapper.Mapper.Map<TSource, TDestination>(source, destination);
        }
    }
}
