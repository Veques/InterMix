using Intermix.Commands;
using Intermix.Enums;
using Intermix.Models;
using Intermix.ViewModels.Base;
using Intermix.ViewModels.MainWindowView.TicTacToeView;
using Intermix.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Intermix.ViewModels.LoginPage
{
    public class LoginPageViewModel : BaseViewModel
    {
        public ICommand LogIn { get; set; }

        public LoginPageViewModel()
        {
            LogIn = new RelayCommand(o => LogInCommand(Username, Password),
                                     o => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password));
        }
        private static void LogInCommand(string Username, string Password)
        {
            ApplicationDbContext _dbContext = new();

            var isValid = _dbContext.Users.FirstOrDefault(u => u.Username == Username && u.Password == Password);

            if (!_dbContext.Users.Any(o => o.Username == "Admin"))
            {
            _dbContext.Users.Add(new Users
            {
                Name = "",
                Surname = "",
                Username = "Admin",
                Password = "1234",
            });

            _dbContext.SaveChanges();
            }

            if (isValid == null)
            {
               _ = new CustomizedMessageBox("Passwords do not match", MessageType.Error, MessageButton.Ok).ShowDialog();
            }
            else
            {
                UserId = isValid.Id;
                User = $"{isValid.Name} {isValid.Surname}";

                foreach (Window item in Application.Current.Windows)
                {
                    item.Hide();
                }

                LibraryMainWindow window = new();
                window.Show();
            }
        }

        public static string? User { get; private set; }
        public static int UserId { get; private set; }

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
