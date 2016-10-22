using Autofac;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data.Contracts;
using Ceriyo.Infrastructure.WPF.Factory.Contracts;
using Ceriyo.Infrastructure.WPF.Observables.Contracts;

namespace Ceriyo.Infrastructure.WPF.Factory
{
    public class ObservableDataFactory: IObservableDataFactory
    {
        private readonly IComponentContext _context;
        private readonly IObjectMapper _objectMapper;
        
        public ObservableDataFactory(IComponentContext context,
            IObjectMapper objectMapper)
        {
            _context = context;
            _objectMapper = objectMapper;
        }

        public T Create<T>() 
            where T : IDataObservable
        {
            return _context.Resolve<T>();
        }

        public TObservable CreateAndMap<TObservable, TData>(TData mapObject) 
            where TObservable : IDataObservable 
            where TData : IDataDomainObject
        {
            TObservable obj = _context.Resolve<TObservable>();
            return _objectMapper.Map(mapObject, obj);
        }
    }
}
