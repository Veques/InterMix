using Intermix.Commands;
using Intermix.Models;
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
    public class CreateAccountViewModel : BaseViewModel
    {

        public ICommand CreateAccount { get; set; }

        public CreateAccountViewModel()
        {
            CreateAccount = new RelayCommand(
                o => CreateAnAccount(Name, Surname, Username, Password),
                o => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname) && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password));
        }

        private void CreateAnAccount(string name, string surname, string username, string password)
        {

            ApplicationDbContext _dbContext = new ApplicationDbContext();
            _dbContext.Database.EnsureCreated();

            try
            {
                Users user = new()
                {
                    Name = name,
                    Surname = surname,
                    Password = password,
                    Username = username
                };

                _dbContext.Users.Add(user);
                if (_dbContext.SaveChanges() > 0)
                {
                    MessageBox.Show("User has been added to db");
                }


            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
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
