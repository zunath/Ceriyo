﻿using System;
using System.Collections.ObjectModel;
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
            DeleteFailureCommand = new RelayCommand(DeleteFailure);

            _characterCreationVM = _vmFactory.Create<CharacterCreationUIViewModel>();
            _characterCreationVM.CharacterSelectionVM = this;

            _networkService.BindPacketAction<CharacterDeletedPacket>(OnCharacterDeletedPacket);
        }

        private readonly CharacterCreationUIViewModel _characterCreationVM;

        public string ServerName { get; set; }
        public string Announcement { get; set; }
        public GameCategory Category { get; set; }
        public PVPType PVP { get; set; }
        public int CurrentPlayers { get; set; }
        public int MaxPlayers { get; set; }

        private bool _isCharacterDeletionEnabled;

        public bool IsCharacterDeletionEnabled
        {
            get => _isCharacterDeletionEnabled;
            set => SetProperty(ref _isCharacterDeletionEnabled, value);
        }
        
        private ObservableCollection<PCTransferObject> _pcs;

        public ObservableCollection<PCTransferObject> PCs
        {
            get => _pcs;
            set => SetProperty(ref _pcs, value);
        }

        private PCTransferObject _selectedPC;

        public PCTransferObject SelectedPC
        {
            get => _selectedPC;
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
            header += "IP Address: " + _networkService.GetServerIPAddress() + Environment.NewLine;
            header += "Category: " + Category + Environment.NewLine;
            header += "PVP: " + PVP + Environment.NewLine;
            header += "Players: " + CurrentPlayers + " / " + MaxPlayers + Environment.NewLine;
            header += Environment.NewLine;
            header += "Announcement: " + Announcement + Environment.NewLine;

            ServerInformationDetails = header;
        }

        private void OnCharacterDeletedPacket(PacketBase p)
        {
            CharacterDeletedPacket packet = (CharacterDeletedPacket)p;
            PCTransferObject pc = PCs.SingleOrDefault(x => x.GlobalID == packet.PCGlobalID);

            if (pc == null) return;

            switch (packet.FailureType)
            {
                case DeleteCharacterFailureType.ServerDoesNotAllowDeletion:
                    IsCharacterDeletionEnabled = false;
                    MessageBox.Show("Unable to delete character. This server doesn't allow character deletion.", "Deletion Failure!", DeleteFailureCommand, false);
                    break;
                case DeleteCharacterFailureType.Success:
                    PCs.Remove(pc);
                    SelectedPC = null;
                    MessageBox.Show("Character deleted successfully!", "Success!", DeleteFailureCommand, false);
                    break;
                default:
                    MessageBox.Show("Unable to delete character. Please try again later.", "Deletion Failure!", DeleteFailureCommand, false);
                    break;
            }

        }
        
        public ICommand DeleteFailureCommand { get; set; }

        private void DeleteFailure(object obj)
        {
            
        }
    }
}
