using Intermix.Commands;
using Intermix.Enums;
using Intermix.Models;
using Intermix.ViewModels.Base;
using Intermix.Views;
using System;
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

                o => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname) &&
                     !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) &&
                     !string.IsNullOrEmpty(PasswordConfirm));
        }

        private void CreateAnAccount(string name, string surname, string username, string password)
        {

            ApplicationDbContext _dbContext = new ApplicationDbContext();

            try
            {
                if (!Password.Equals(PasswordConfirm))
                {
                    _ = new CustomizedMessageBox("Passwords do not match", MessageType.Error, MessageButton.Ok).ShowDialog();

                    return;
                }

                if (Password.Length < 8)
                {
                    _ = new CustomizedMessageBox("Password is to short", MessageType.Warning, MessageButton.Ok).ShowDialog();

                    return;
                }

                _dbContext.Database.EnsureCreated();
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
                    _ = new CustomizedMessageBox("You have been added to database", MessageType.Success, MessageButton.Ok).ShowDialog();

                }

                Name = String.Empty;
                Surname = String.Empty;
                Username = String.Empty;


            }
            catch (Exception ex)
            {
                _ = new CustomizedMessageBox(ex.Message, MessageType.Error, MessageButton.Ok).ShowDialog();

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

        private string _passwordConfirm;
        public string PasswordConfirm
        {
            get { return _passwordConfirm; }
            set
            {
                _passwordConfirm = value;
                OnPropertyChanged("PasswordConfirm");
            }
        }


    }
}
