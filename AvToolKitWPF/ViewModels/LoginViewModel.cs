using AvToolKitWPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using AvToolKitWPF.Views;

namespace AvToolKitWPF.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly ILoginService _loginService;
        public string Username { get; set; }
        public ICommand LoginCommand { get; }
        public LoginViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            LoginCommand = new RelayCommand<PasswordBox>(async (passwordBox) => await Login(passwordBox));
        }
        private async Task Login(PasswordBox passwordBox)
        {
            try
            {
                var token = await _loginService.Auth(Username, passwordBox.Password);
                
                var mainWindow = new MainWindow();
                mainWindow.Show();

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Login failed: " + ex.Message);
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
