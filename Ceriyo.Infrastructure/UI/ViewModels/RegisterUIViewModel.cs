using System.Collections.Generic;
using System.Net.Http;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.UI.Contracts;
using EmptyKeys.UserInterface.Generated;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;
using System;
using Ceriyo.Infrastructure.UI.Generated;

namespace Ceriyo.Infrastructure.UI.ViewModels
{
    public class RegisterUIViewModel: ViewModelBase, IUIViewModel
    {
        private readonly IUIService _uiService;
        private readonly IUIViewModelFactory _vmFactory;
        

        public RegisterUIViewModel(IUIService uiService, IUIViewModelFactory vmFactory)
        {
            _uiService = uiService;
            _vmFactory = vmFactory;
            
            RegisterCommand = new RelayCommand(Register);
            CancelCommand = new RelayCommand(Cancel);

            IsEnabled = true;
        }

        private string _username;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _email;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private string _infoText;

        public string InfoText
        {
            get => _infoText;
            set => SetProperty(ref _infoText, value);
        }

        public ICommand RegisterCommand { get; set; }

        private async void Register(object obj)
        {
            IsEnabled = false;
            InfoText = "Registering account. Please wait...";

            HttpClient client = new HttpClient();

            var data = new Dictionary<string, string>
            {
                {"Username", Username },
                {"Password", Password },
                {"ConfirmPassword", ConfirmPassword },
                {"Email", Email }
            };
            
            var content = new FormUrlEncodedContent(data);

            var response = await client.PostAsync(Urls.AuthServerUrl + "api/Account/Register", content);

            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Registration response: " + responseString); // DEBUG

            IsEnabled = true;
            InfoText = string.Empty;
        }
        
        public ICommand CancelCommand { get; set; }

        private void Cancel(object obj)
        {
            var model = _vmFactory.Create<LoginUIViewModel>();
            _uiService.ChangeUIRoot<LoginView>(model);
        }
        

    }
}
