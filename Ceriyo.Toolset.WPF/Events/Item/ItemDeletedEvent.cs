using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.Observables;
using Prism.Events;

namespace Ceriyo.Toolset.WPF.Events.Item
{
    public class ItemDeletedEvent: PubSubEvent<ItemDataObservable>
    {
    }
}
