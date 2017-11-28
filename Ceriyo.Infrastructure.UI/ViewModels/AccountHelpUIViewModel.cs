using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.UI.Contracts;
using Ceriyo.Infrastructure.UI.ViewModels.Validation;
using EmptyKeys.UserInterface;
using EmptyKeys.UserInterface.Controls;
using EmptyKeys.UserInterface.Generated;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;

namespace Ceriyo.Infrastructure.UI.ViewModels
{
    public class AccountHelpUIViewModel: ViewModelBase, IUIViewModel
    {
        private readonly IUIService _uiService;
        private readonly IUIViewModelFactory _vmFactory;
        private readonly ILogger _logger;

        private readonly AccountHelpUIViewModelValidator _validator;

        public AccountHelpUIViewModel(
            IUIService uiService,
            IUIViewModelFactory vmFactory,
            ILogger logger)
        {
            _uiService = uiService;
            _vmFactory = vmFactory;
            _logger = logger;

            ConfirmCommand = new RelayCommand(Confirm);
            BackCommand = new RelayCommand(Back);

            ConfirmCloseCommand = new RelayCommand(ConfirmClose);

            _validator = new AccountHelpUIViewModelValidator();

            IsEnabled = true;
            BuildHelpOptions();
        }

        private void BuildHelpOptions()
        {
            HelpOptions = new ObservableCollection<ComboBoxItem>
            {
                new ComboBoxItem {Content = "I forgot my password."},
                new ComboBoxItem {Content = "Resend my account verification code."}
            };


        }

        private ObservableCollection<ComboBoxItem> _helpOptions;

        public ObservableCollection<ComboBoxItem> HelpOptions
        {
            get => _helpOptions;
            set => SetProperty(ref _helpOptions, value);
        }

        private int _selectedHelpOptionIndex;

        public int SelectedHelpOptionIndex
        {
            get => _selectedHelpOptionIndex;
            set
            {
                SetProperty(ref _selectedHelpOptionIndex, value);
                HandleOptionChange();
            } 
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

        private string _confirmButtonText;

        public string ConfirmButtonText
        {
            get => _confirmButtonText;
            set => SetProperty(ref _confirmButtonText, value);
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private void HandleOptionChange()
        {
            ConfirmButtonText = SelectedHelpOptionIndex == 0 ? "Reset Password" : "Resend Verification Email";
        }

        public ICommand ConfirmCommand { get; set; }

        private void Confirm(object obj)
        {
            if (!ValidateModel()) return;

            if(SelectedHelpOptionIndex == 0)
                ConfirmResetPassword();
            else 
                ConfirmResendVerification();
        }

        public ICommand BackCommand { get; set; }

        private void Back(object obj)
        {
            var vm = _vmFactory.Create<LoginUIViewModel>();
            _uiService.ChangeUIRoot<LoginView>(vm);
        }

        private bool ValidateModel()
        {
            if (_validator.Validate(this).IsValid) return true;

            MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButton.OK, null, false);

            return false;
        }

        private async void ConfirmResetPassword()
        {
            
        }

        private async void ConfirmResendVerification()
        {
            IsEnabled = false;
            HttpClient client = new HttpClient();

            var data = new Dictionary<string, string>
            {
                { "Username", Username },
                { "Email", Email }
            };

            var content = new FormUrlEncodedContent(data);

            try
            {
                var response = await client.PostAsync(Urls.AuthServerUrl + "api/Account/ResendVerification", content);
                IsEnabled = true;

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Your account verification code has been sent to your email. Please check your spam/junk folder if you do not see it.", "Verification Sent", MessageBoxButton.OK, ConfirmCloseCommand, false);

                }
                else
                {
                    MessageBox.Show("Unable to resend verification code. Is your username and email correct?", "Error", MessageBoxButton.OK, null, false);
                }

            }
            catch (Exception ex)
            {
                _logger.Error($"Error logging in. ({nameof(AccountHelpUIViewModel)} -> {nameof(ConfirmResendVerification)}", ex);
                MessageBox.Show("There was an error reaching the master server. Please check your internet connection.", "Error Connecting", MessageBoxButton.OK, null, false);
                IsEnabled = true;
            }

        }
        
        private ICommand ConfirmCloseCommand { get; }

        private void ConfirmClose(object obj)
        {
            LoginUIViewModel vm = _vmFactory.Create<LoginUIViewModel>();
            _uiService.ChangeUIRoot<LoginView>(vm);
        }

    }
}
