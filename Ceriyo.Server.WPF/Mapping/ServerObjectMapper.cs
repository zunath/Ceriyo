using AutoMapper;
using Ceriyo.Core.Data;
using IObjectMapper = Ceriyo.Core.Contracts.IObjectMapper;

namespace Ceriyo.Server.WPF.Mapping
{
    public class ServerObjectMapper: IObjectMapper
    {
        public void Initialize()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<ModuleData, ModuleData>();
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
