using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Infrastructure.Network.Packets;
using Ceriyo.Infrastructure.UI.Contracts;
using EmptyKeys.UserInterface.Generated;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;

namespace Ceriyo.Infrastructure.UI.ViewModels
{
    public class DirectConnectUIViewModel: ViewModelBase, IUIViewModel
    {
        private readonly IUIService _uiService;
        private readonly IUIViewModelFactory _vmFactory;
        private readonly IClientNetworkService _networkService;

        public string IPAddress { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }


        public DirectConnectUIViewModel(IUIService uiService,
            IUIViewModelFactory vmFactory,
            IClientNetworkService networkService)
        {
            _uiService = uiService;
            _vmFactory = vmFactory;
            _networkService = networkService;

            BackCommand = new RelayCommand(Back);
            ConnectCommand = new RelayCommand(Connect);

            networkService.PacketReceived += OnPacketReceived;

            // DEBUGGING

            IPAddress = "127.0.0.1"; // localhost for testing
            Port = 5121; // Default Port

            // END DEBUGGING

        }

        private void OnPacketReceived(PacketBase p)
        {
            if (p.GetType() == typeof(ConnectedToServerPacket))
            {
                ConnectedToServerPacket packet = (ConnectedToServerPacket) p;

            }
        }

        public ICommand BackCommand { get; set; }

        private void Back(object obj)
        {
            MainMenuUIViewModel vm = _vmFactory.Create<MainMenuUIViewModel>();
            _uiService.ChangeUIRoot<MainMenu>(vm);
        }

        public ICommand ConnectCommand { get; set; }

        private void Connect(object obj)
        {
            _networkService.ConnectToServer(IPAddress, Port, "zunath", Password);
        }

    }
}
