using System;
using Prism.Events;

namespace Ceriyo.Toolset.WPF.Events.Error
{
    public class DetailedErrorEvent: PubSubEvent<Tuple<string, string>>
    {
    }
}
