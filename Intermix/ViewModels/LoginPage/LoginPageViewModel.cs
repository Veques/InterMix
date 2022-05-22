using Intermix.Commands;
using Intermix.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Intermix.ViewModels.LoginPage
{
    public class LoginPageViewModel : BaseViewModel
    {
        public ICommand LogIn { get; set; }
        public ICommand CreateAccount { get; set; }

        public LoginPageViewModel()
        {
            LogIn = new RelayCommand(o => LogInCommand(Username, Password), o => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password));
            CreateAccount = new RelayCommand(o => NewAccount(), o => true);

        }
        private void NewAccount()
        {

        }
        private void LogInCommand(string Username, string Password)
        {
            MessageBox.Show(Password);
        }

        private string _username;

        public string Username

        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }

        private string _password;

        public string Password

        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }


    }
}
