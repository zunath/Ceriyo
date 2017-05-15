using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.Network.Contracts;
using EmptyKeys.UserInterface.Mvvm;

namespace Ceriyo.Infrastructure.UI.ViewModels
{
    public class GameUIViewModel: ViewModelBase, IUIViewModel
    {
        private IClientNetworkService _networkService;

        public GameUIViewModel(IClientNetworkService networkService)
        {
            _networkService = networkService;
        }

    }
}
