using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Infrastructure.Network.Packets;
using Ceriyo.Infrastructure.Network.Packets.Connection;
using Ceriyo.Infrastructure.Network.TransferObjects;
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
        private readonly IPathService _pathService;

        public string IPAddress { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }


        public DirectConnectUIViewModel(IUIService uiService,
            IUIViewModelFactory vmFactory,
            IClientNetworkService networkService,
            IPathService pathService)
        {
            _uiService = uiService;
            _vmFactory = vmFactory;
            _networkService = networkService;
            _pathService = pathService;

            BackCommand = new RelayCommand(Back);
            ConnectCommand = new RelayCommand(Connect);

            networkService.OnPacketReceived += PacketReceived;

            // DEBUGGING

            IPAddress = "127.0.0.1"; // localhost for testing
            Port = 5121; // Default Port

            // END DEBUGGING

        }

        private void PacketReceived(PacketBase p)
        {
            if (p.GetType() == typeof(ConnectedToServerPacket))
            {
                var packet = (ConnectedToServerPacket) p;
                
                foreach (string resourcePack in packet.RequiredResourcePacks)
                {
                    if (!File.Exists(_pathService.ResourcePackDirectory + resourcePack + ".rpk"))
                    {
                        _networkService.DisconnectFromServer($"Missing required resource packs. (File: {resourcePack})");
                        return;
                    }
                }


                var vm = _vmFactory.Create<CharacterSelectionUIViewModel>();
                vm.IsCharacterDeletionEnabled = packet.AllowCharacterDeletion;
                vm.ServerName = packet.ServerName;
                vm.Announcement = packet.Announcement;
                vm.Category = packet.Category;
                vm.PVP = packet.PVP;
                vm.CurrentPlayers = packet.CurrentPlayers;
                vm.MaxPlayers = packet.MaxPlayers;
                vm.IPAddress = packet.IPAddress;

                foreach (var pc in packet.PCs)
                {
                    vm.PCs.Add(pc);
                }

                vm.BuildServerInformationDetails();

                _uiService.ChangeUIRoot<CharacterSelectionView>(vm);
            }
        }

        public ICommand BackCommand { get; set; }

        private void Back(object obj)
        {
            MainMenuUIViewModel vm = _vmFactory.Create<MainMenuUIViewModel>();
            _uiService.ChangeUIRoot<MainMenuView>(vm);
        }

        public ICommand ConnectCommand { get; set; }

        private void Connect(object obj)
        {
            _networkService.ConnectToServer(IPAddress, Port, "zunath", Password);
        }

    }
}
