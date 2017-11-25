using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Infrastructure.Network.Packets;
using Ceriyo.Infrastructure.Network.Packets.CharacterManagement;
using Ceriyo.Infrastructure.UI.Contracts;
using Ceriyo.Infrastructure.UI.ViewModels;
using EmptyKeys.UserInterface.Generated;

namespace Ceriyo.Game.Windows.Screens
{
    public class MainMenuScreen: IScreen
    {
        private readonly IUIViewModelFactory _uiViewModelFactory;
        private readonly IUIService _uiService;
        private readonly IScreenService _screenService;

        public MainMenuScreen(IUIViewModelFactory viewModelFactory,
            IUIService uiService,
            IClientNetworkService networkService,
            IScreenService screenService)
        {
            _uiViewModelFactory = viewModelFactory;
            _uiService = uiService;
            _screenService = screenService;

            networkService.BindPacketAction<CharacterAddedToWorldPacket>(OnCharacterAddedToWorldPacket);
        }

        public void Initialize()
        {
            MainMenuUIViewModel vm = _uiViewModelFactory.Create<MainMenuUIViewModel>();
            _uiService.ChangeUIRoot<MainMenuView>(vm);
        }

        public void Update()
        {

        }

        public void Draw()
        {

        }

        public void Close()
        {

        }

        private void OnCharacterAddedToWorldPacket(PacketBase p)
        {
            _screenService.ChangeScreen<GameScreen>();
        }
    }
}
