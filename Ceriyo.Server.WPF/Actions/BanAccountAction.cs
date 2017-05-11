using Ceriyo.Core.Contracts;
using Ceriyo.Core.Settings;
using Ceriyo.Server.WPF.Contracts;

namespace Ceriyo.Server.WPF.Actions
{
    public class BanAccountAction: IServerAction
    {
        private readonly ServerSettings _settings;
        private readonly IServerActionService _actionService;
        private readonly IServerActionFactory _actionFactory;

        public BanAccountAction(ServerSettings settings, 
            IServerActionService actionService,
            IServerActionFactory actionFactory)
        {
            _settings = settings;
            _actionService = actionService;
            _actionFactory = actionFactory;
        }

        public string Username { get; set; }

        public void Process()
        {
            if (string.IsNullOrWhiteSpace(Username)) return;

            var action = _actionFactory.Create<BootPlayerAction>();
            action.Username = Username;
            _actionService.QueueAction(action);

            if (_settings.Blacklist.Contains(Username)) return;

            // TODO: BindingList is not threadsafe
            _settings.Blacklist.Add(Username);
        }
    }
}
