using Intermix.Commands;
using Intermix.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Intermix.ViewModels.LoginPage
{
    public class CreateAccountViewModel : BaseViewModel
    {

        public ICommand CreateAccount { get; set; }

        public CreateAccountViewModel()
        {
            CreateAccount = new RelayCommand(
                o => CreateAnAccount(Name, Surname, Login, Password),
                o => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname) && !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password));
        }

        private void CreateAnAccount(string name, string surname, string login, string password)
        {

        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _surname;

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged("Surname");
            }
        }

        private string _login;

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
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
