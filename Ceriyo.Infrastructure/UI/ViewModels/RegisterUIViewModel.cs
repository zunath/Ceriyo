using System.Collections.Generic;
using System.Net.Http;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.UI.Contracts;
using EmptyKeys.UserInterface.Generated;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;
using System;
using System.Linq;
using Ceriyo.Infrastructure.UI.ViewModels.Validation;
using EmptyKeys.UserInterface;
using EmptyKeys.UserInterface.Media;
using Newtonsoft.Json.Linq;

namespace Ceriyo.Infrastructure.UI.ViewModels
{
    public class RegisterUIViewModel: ViewModelBase, IUIViewModel
    {
        private readonly IUIService _uiService;
        private readonly IUIViewModelFactory _vmFactory;
        private readonly ILogger _logger;
        private readonly RegisterUIViewModelValidator _validator;

        public RegisterUIViewModel(IUIService uiService, 
            IUIViewModelFactory vmFactory,
            ILogger logger)
        {
            _uiService = uiService;
            _vmFactory = vmFactory;
            _logger = logger;
            
            RegisterCommand = new RelayCommand(Register);
            CancelCommand = new RelayCommand(Cancel);
            _validator = new RegisterUIViewModelValidator();

            IsEnabled = true;

            Username = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            Email = string.Empty;
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

        private SolidColorBrush _colorBrush;

        public SolidColorBrush InfoTextBrush
        {
            get => _colorBrush;
            set => SetProperty(ref _colorBrush, value);
        }

        public ICommand RegisterCommand { get; set; }

        private async void Register(object obj)
        {
            InfoTextBrush = Brushes.Green;
            IsEnabled = false;
            InfoText = "Registering account. Please wait...";

            if (!ValidateModel())
            {
                InfoTextBrush = Brushes.Red;
                InfoText = "Invalid data has been entered. Please check above for more details.";
                IsEnabled = true;
                return;
            }


            HttpClient client = new HttpClient();

            var data = new Dictionary<string, string>
            {
                {"Username", Username },
                {"Password", Password },
                {"ConfirmPassword", ConfirmPassword },
                {"Email", Email }
            };
            
            var content = new FormUrlEncodedContent(data);

            try
            {
                var response = await client.PostAsync(Urls.AuthServerUrl + "api/Account/Register", content);
                var responseString = await response.Content.ReadAsStringAsync();

                IsEnabled = true;
                InfoText = string.Empty;
                InfoTextBrush = Brushes.Green;

                if (response.IsSuccessStatusCode)
                {
                    var model = _vmFactory.Create<LoginUIViewModel>();
                    model.Notification = "Please check your email for a link to verify your account.";
                    _uiService.ChangeUIRoot<LoginView>(model);
                }
                else
                {
                    var json = JObject.Parse(responseString);

                    var errors = new Dictionary<string, string[]>();
                    var modelState = json["ModelState"];
                    foreach (var errorProperty in modelState.OfType<JProperty>())
                    {
                        string key = errorProperty.Name.Remove(0, 6); // Remove "model." prefix
                        var values = new List<string>();
                        foreach (var error in errorProperty.Values())
                        {
                            values.Add(error.ToString());
                        }

                        errors.Add(key, values.ToArray());
                    }


                    string errorMessage = $"There was a problem with creating your account. Please fix the problems and try again.{Environment.NewLine}{Environment.NewLine}";
                    foreach (var error in errors)
                    {
                        errorMessage += error.Key + Environment.NewLine;
                        foreach (var detail in error.Value)
                        {
                            errorMessage += "\t" + detail + Environment.NewLine;
                        }
                    }

                    MessageBox.Show(errorMessage, "Error Creating Account", MessageBoxButton.OK, null, false);
                }

            }
            catch (Exception ex)
            {
                IsEnabled = true;
                InfoText = $"There was an error reaching the master server.{Environment.NewLine} Please check your internet connection.";
                InfoTextBrush = Brushes.Red;

                _logger.Error($"Error connecting to master server. ({nameof(RegisterUIViewModel)} -> {nameof(Register)}", ex);
            }

        }
        
        public ICommand CancelCommand { get; set; }

        private void Cancel(object obj)
        {
            var model = _vmFactory.Create<LoginUIViewModel>();
            _uiService.ChangeUIRoot<LoginView>(model);
        }


        private bool ValidateModel()
        {
            return _validator.Validate(this).IsValid;
        }

    }
}
