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

        public string AccountName { get; set; }

        public void Process()
        {
            if (string.IsNullOrWhiteSpace(AccountName)) return;

            var action = _actionFactory.Create<BootPlayerAction>();
            action.AccountName = AccountName;
            _actionService.QueueAction(action);

            if (_settings.Blacklist.Contains(AccountName)) return;
            _settings.Blacklist.Add(AccountName);
        }
    }
}
