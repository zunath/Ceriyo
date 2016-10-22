using Ceriyo.Core.Data.Contracts;
using Ceriyo.Infrastructure.WPF.Observables.Contracts;

namespace Ceriyo.Infrastructure.WPF.Factory.Contracts
{
    public interface IObservableDataFactory
    {
        T Create<T>()
            where T: IDataObservable;

        TObservable CreateAndMap<TObservable, TData>(TData mapObject)
            where TObservable: IDataObservable
            where TData: IDataDomainObject;
    }
}
