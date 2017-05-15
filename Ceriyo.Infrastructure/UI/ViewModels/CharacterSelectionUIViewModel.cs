﻿using System;
using System.Collections.Generic;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Infrastructure.Network.Packets.CharacterManagement;
using Ceriyo.Infrastructure.Network.TransferObjects;
using Ceriyo.Infrastructure.UI.Contracts;
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

            CreateCharacterCommand = new RelayCommand(CreateCharacter);
            DeleteCharacterCommand = new RelayCommand(DeleteCharacter);
            DisconnectCommand = new RelayCommand(Disconnect);
            JoinServerCommand = new RelayCommand(JoinServer);
        }
        
        public string ServerName { get; set; }
        public string Announcement { get; set; }
        public GameCategory Category { get; set; }
        public PVPType PVP { get; set; }
        public int CurrentPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public string IPAddress { get; set; }
        public bool IsCharacterDeletionEnabled { get; set; }

        public List<PCTransferObject> PCs { get; set; }

        private PCTransferObject _selectedPC;

        public PCTransferObject SelectedPC
        {
            get
            {
                return _selectedPC;
            }
            set
            {
                _selectedPC = value;
                RaisePropertyChanged("IsPCSelected");
            }
        }

        public bool IsPCSelected => SelectedPC != null;

        public ICommand CreateCharacterCommand { get; set; }

        public string ServerInformationDetails { get; set; }

        private void CreateCharacter(object obj)
        {
            var vm = _vmFactory.Create<CharacterCreationUIViewModel>();
            vm.CharacterSelectionVM = this;
            _uiService.ChangeUIRoot<CharacterCreationView>(vm);
        }

        public ICommand DeleteCharacterCommand { get; set; }

        private void DeleteCharacter(object obj)
        {
            
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
    }
}
