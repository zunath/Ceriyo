using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Infrastructure.Network.Packets;
using Ceriyo.Infrastructure.Network.Packets.CharacterManagement;
using Ceriyo.Infrastructure.Network.TransferObjects;
using Ceriyo.Infrastructure.UI.Contracts;
using EmptyKeys.UserInterface;
using EmptyKeys.UserInterface.Generated;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;

namespace Ceriyo.Infrastructure.UI.ViewModels
{
    public class CharacterSelectionUIViewModel: ViewModelBase, IUIViewModel
    {
        private readonly IUIService _uiService;
        private readonly IUIViewModelFactory _vmFactory;
        private readonly IClientNetworkService _networkService;

        public CharacterSelectionUIViewModel(IUIService uiService,
            IUIViewModelFactory vmFactory,
            IClientNetworkService networkService)
        {
            _uiService = uiService;
            _vmFactory = vmFactory;
            _networkService = networkService;

            PCs = new ObservableCollection<PCTransferObject>();

            CreateCharacterCommand = new RelayCommand(CreateCharacter);
            DeleteCharacterCommand = new RelayCommand(DeleteCharacter);
            DisconnectCommand = new RelayCommand(Disconnect);
            JoinServerCommand = new RelayCommand(JoinServer);
            ConfirmDeleteCharacterCommand = new RelayCommand(ConfirmDeleteCharacter);

            _characterCreationVM = _vmFactory.Create<CharacterCreationUIViewModel>();
            _characterCreationVM.CharacterSelectionVM = this;
            _networkService.OnPacketReceived += PacketReceived;
        }

        private readonly CharacterCreationUIViewModel _characterCreationVM;

        public string ServerName { get; set; }
        public string Announcement { get; set; }
        public GameCategory Category { get; set; }
        public PVPType PVP { get; set; }
        public int CurrentPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public string IPAddress { get; set; }
        public bool IsCharacterDeletionEnabled { get; set; }
        
        private ObservableCollection<PCTransferObject> _pcs;

        public ObservableCollection<PCTransferObject> PCs
        {
            get { return _pcs; }
            set { SetProperty(ref _pcs, value); }
        }

        private PCTransferObject _selectedPC;

        public PCTransferObject SelectedPC
        {
            get
            {
                return _selectedPC;
            }
            set
            {
                SetProperty(ref _selectedPC, value);
                RaisePropertyChanged("IsPCSelected");
            }
        }
        
        public bool IsPCSelected => SelectedPC != null;

        public ICommand CreateCharacterCommand { get; set; }

        public string ServerInformationDetails { get; set; }

        private void CreateCharacter(object obj)
        {
            _uiService.ChangeUIRoot<CharacterCreationView>(_characterCreationVM);
        }

        public ICommand DeleteCharacterCommand { get; set; }

        private void DeleteCharacter(object obj)
        {
            if (SelectedPC == null) return;
            
            MessageBox.Show("Are you sure you want to delete this character?", "Delete Character?", MessageBoxButton.YesNo, ConfirmDeleteCharacterCommand, false);
        }

        public ICommand ConfirmDeleteCharacterCommand;

        private void ConfirmDeleteCharacter(object obj)
        {
            MessageBoxResult result = (MessageBoxResult) obj;

            if (result == MessageBoxResult.Yes)
            {
                DeleteCharacterPacket packet = new DeleteCharacterPacket
                {
                    PCGlobalID = SelectedPC.GlobalID
                };

                _networkService.SendMessage(PacketDeliveryMethod.ReliableUnordered, packet);
            }
        }

        public ICommand DisconnectCommand { get; set; }

        private void Disconnect(object obj)
        {
            _networkService.DisconnectFromServer();
            var vm = _vmFactory.Create<MainMenuUIViewModel>();
            _uiService.ChangeUIRoot<MainMenuView>(vm);
        }

        public ICommand JoinServerCommand { get; set; }

        private void JoinServer(object obj)
        {
            CharacterSelectedPacket packet = new CharacterSelectedPacket
            {
                PCGlobalID = SelectedPC.GlobalID
            };

            _networkService.SendMessage(PacketDeliveryMethod.ReliableUnordered, packet);
        }

        public void BuildServerInformationDetails()
        {
            string header = string.Empty;
            header += "Server Name: " + ServerName + Environment.NewLine;
            header += "IP Address: " + IPAddress + Environment.NewLine;
            header += "Category: " + Category + Environment.NewLine;
            header += "PVP: " + PVP + Environment.NewLine;
            header += "Players: " + CurrentPlayers + " / " + MaxPlayers + Environment.NewLine;
            header += Environment.NewLine;
            header += "Announcement: " + Announcement + Environment.NewLine;

            ServerInformationDetails = header;
        }
        
        private void PacketReceived(PacketBase p)
        {
            Type type = p.GetType();

            if (type == typeof(CharacterDeletedPacket))
            {
                CharacterDeletedPacket packet = (CharacterDeletedPacket) p;
                PCTransferObject pc = PCs.SingleOrDefault(x => x.GlobalID == packet.PCGlobalID);

                if (pc == null) return;

                PCs.Remove(pc);
                SelectedPC = null;
            }
        }
    }
}
