using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.Observables;
using Prism.Events;

namespace Ceriyo.Toolset.WPF.Events.Skill
{
    public class SkillDeletedEvent: PubSubEvent<SkillDataObservable>
    {
    }
}
