using System;
using System.Collections.Generic;
using System.Net.Http;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.UI.Contracts;
using Ceriyo.Infrastructure.UI.ViewModels.Validation;
using EmptyKeys.UserInterface;
using EmptyKeys.UserInterface.Generated;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;
using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;

namespace Ceriyo.Infrastructure.UI.ViewModels
{
    public class LoginUIViewModel: ViewModelBase, IUIViewModel
    {
        private readonly Game _game;
        private readonly IUIService _uiService;
        private readonly IUIViewModelFactory _vmFactory;
        private readonly IUserProfile _userProfile;
        private readonly ILogger _logger;
        private readonly LoginUIViewModelValidator _validator;

        public LoginUIViewModel(Game game, 
            IUIService uiService, 
            IUIViewModelFactory vmFactory,
            IUserProfile userProfile,
            ILogger logger)
        {
            _game = game;
            _uiService = uiService;
            _vmFactory = vmFactory;
            _userProfile = userProfile;
            _logger = logger;

            LoginCommand = new RelayCommand(Login);
            CreateAccountCommand = new RelayCommand(CreateAccount);
            AccountHelpCommand = new RelayCommand(AccountHelp);
            ExitCommand = new RelayCommand(Exit);

            _validator = new LoginUIViewModelValidator();

            IsEnabled = true;
        }

        private string _username;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _notification;

        public string Notification
        {
            get => _notification;
            set => SetProperty(ref _notification, value);
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public ICommand LoginCommand { get; set; }

        private async void Login(object obj)
        {
            if (!ValidateModel())
            {
                MessageBox.Show("Please enter a username and password.", "Error", MessageBoxButton.OK, null, false);
                return;
            }

            IsEnabled = false;
            HttpClient client = new HttpClient();

            var data = new Dictionary<string, string>
            {
                { "username", Username },
                { "password", Password },
                { "grant_type", "password" }
            };

            var content = new FormUrlEncodedContent(data);

            try
            {
                var response = await client.PostAsync(Urls.AuthServerUrl + "oauth/token", content);
                var responseString = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(responseString);
                IsEnabled = true;

                if (response.IsSuccessStatusCode)
                {
                    _userProfile.MasterServerToken = json["access_token"].Value<string>();
                    var model = _vmFactory.Create<MainMenuUIViewModel>();
                    _uiService.ChangeUIRoot<MainMenuView>(model);
                }
                else
                {
                    MessageBox.Show("Invalid username and/or password. Please try again.", "Invalid Credentials", MessageBoxButton.OK, null, false);
                }

            }
            catch (Exception ex)
            {
                _logger.Error($"Error logging in. ({nameof(LoginUIViewModel)} -> {nameof(Login)}", ex);
                MessageBox.Show("There was an error reaching the master server. Please check your internet connection.", "Error Connecting", MessageBoxButton.OK, null, false);
                IsEnabled = true;
            }

        }

        public ICommand CreateAccountCommand { get; set; }

        private void CreateAccount(object obj)
        {
            RegisterUIViewModel vm = _vmFactory.Create<RegisterUIViewModel>();
            _uiService.ChangeUIRoot<RegisterView>(vm);
        }

        public ICommand AccountHelpCommand { get; set; }

        private void AccountHelp(object obj)
        {
            
        }

        public ICommand ExitCommand { get; set; }

        private void Exit(object obj)
        {
            _game.Exit();
        }

        private bool ValidateModel()
        {
            return _validator.Validate(this).IsValid;
        }

    }
}
