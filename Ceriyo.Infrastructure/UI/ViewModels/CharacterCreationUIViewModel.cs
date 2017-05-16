using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Infrastructure.Network.Packets;
using Ceriyo.Infrastructure.Network.Packets.CharacterManagement;
using Ceriyo.Infrastructure.Network.TransferObjects;
using Ceriyo.Infrastructure.UI.Contracts;
using EmptyKeys.UserInterface.Generated;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;

namespace Ceriyo.Infrastructure.UI.ViewModels
{
    public class CharacterCreationUIViewModel: ViewModelBase, IUIViewModel
    {
        private readonly IUIService _uiService;
        private readonly IUIViewModelFactory _vmFactory;
        private readonly IClientNetworkService _networkService;
        
        public CharacterSelectionUIViewModel CharacterSelectionVM { get; set; }

        public CharacterCreationUIViewModel(IUIService uiService,
            IUIViewModelFactory vmFactory,
            IClientNetworkService networkService)
        {
            _uiService = uiService;
            _vmFactory = vmFactory;
            _networkService = networkService;

            CreateCharacterCommand = new RelayCommand(CreateCharacter);
            BackCommand = new RelayCommand(Back);
            DisconnectCommand = new RelayCommand(Disconnect);

            _networkService.OnPacketReceived += PacketReceived;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsModelValid => ValidateModel();

        public ICommand CreateCharacterCommand { get; set; }

        private void CreateCharacter(object obj)
        {
            CreateCharacterPacket packet = new CreateCharacterPacket
            {
                FirstName = FirstName,
                LastName = LastName
            };

            _networkService.SendMessage(PacketDeliveryMethod.ReliableUnordered, packet);
        }

        public ICommand BackCommand { get; set; }

        private void Back(object obj)
        {
            ClearData();
            _uiService.ChangeUIRoot<CharacterSelectionView>(CharacterSelectionVM);
        }

        public ICommand DisconnectCommand { get; set; }

        private void Disconnect(object obj)
        {
            _networkService.DisconnectFromServer();
            var vm = _vmFactory.Create<MainMenuUIViewModel>();
            _uiService.ChangeUIRoot<MainMenuView>(vm);
        }

        private bool ValidateModel()
        {
            return true;
        }

        private void ClearData()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        private void PacketReceived(PacketBase p)
        {
            if (p.GetType() == typeof(CharacterCreatedPacket))
            {
                var packet = (CharacterCreatedPacket) p;

                PCTransferObject pcTO = new PCTransferObject
                {
                    LastName = packet.LastName,
                    FirstName = packet.FirstName,
                    Description = packet.Description,
                    Level = packet.Level,
                    GlobalID = packet.GlobalID
                };

                CharacterSelectionVM.PCs.Add(pcTO);

                ClearData();
                _uiService.ChangeUIRoot<CharacterSelectionView>(CharacterSelectionVM);
            }
        }
    }
}
