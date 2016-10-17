using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.Observables;
using Prism.Events;

namespace Ceriyo.Toolset.WPF.Events.Creature
{
    public class CreatureCreatedEvent: PubSubEvent<CreatureDataObservable>
    {
    }
}
