using System.Collections.Generic;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
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
            
            _networkService.BindPacketAction<CharacterCreatedPacket>(OnCharacterCreatedPacket);
            _networkService.BindPacketAction<CharacterCreationDataPacket>(OnCharacterCreationDataPacket);

            Classes = new List<ClassData>();

            RequestData();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ClassData SelectedClass { get; set; }
        public List<ClassData> Classes { get; set; }

        public bool IsModelValid => ValidateModel();

        public ICommand CreateCharacterCommand { get; set; }

        private void RequestData()
        {
            CharacterCreationDataPacket packet = new CharacterCreationDataPacket
            {
                IsRequest = true
            };

            _networkService.SendMessage(PacketDeliveryMethod.ReliableUnordered, packet);
        }

        private void CreateCharacter(object obj)
        {
            CreateCharacterPacket packet = new CreateCharacterPacket
            {
                FirstName = FirstName,
                LastName = LastName,
                Class = SelectedClass.Resref
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
            SelectedClass = null;
        }

        private void OnCharacterCreatedPacket(PacketBase p)
        {
            var packet = (CharacterCreatedPacket)p;

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

        private void OnCharacterCreationDataPacket(PacketBase p)
        {
            var packet = (CharacterCreationDataPacket)p;

            Classes.Clear();
            foreach (var @class in packet.Classes)
            {
                Classes.Add(@class);
            }
        }
    }
}
