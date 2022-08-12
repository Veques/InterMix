using Intermix.Commands;
using Intermix.Enums;
using Intermix.Models;
using Intermix.Stores;
using Intermix.ViewModels.Base;
using Intermix.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Intermix.ViewModels.LoginPage
{
    public class LoginPageViewModel : BaseViewModel
    {
        public ICommand LogInCommand { get; set; }
        public ICommand CreateAccountCommand { get; set; }

        #region Constructor
        public LoginPageViewModel(NavigationStore navigationStore)
        {
            LogInCommand = new RelayCommand(o => LogIn(Username, Password),
                                     o => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password));

            CreateAccountCommand = new NavigationCommand<CreateAccountViewModel>(navigationStore,
                () => new CreateAccountViewModel(navigationStore),
                x => true);
        }
        #endregion

        private static void LogIn(string Username, string Password)
        {
            ApplicationDbContext _dbContext = new();

            var doesExist = _dbContext.Users.FirstOrDefault(u => u.Username == Username && u.Password == Password);

            if (Username == "Admin")
            {
               _ = new CustomizedMessageBox("You can Log as Admin in App, use yours own account", MessageType.Warning, MessageButton.Ok).ShowDialog();
                Username = string.Empty;
                Password = string.Empty;
                return;
            }

            if (doesExist == null)
            {
               _ = new CustomizedMessageBox("Passwords do not match", MessageType.Error, MessageButton.Ok).ShowDialog();
            }
            else
            {
                UserId = doesExist.Id;
                User = $"{doesExist.Name} {doesExist.Surname}";

                LibraryMainWindow window = new()
                {
                    Name = "LibraryMainWindow"
                };
                window.Show();

                foreach (Window item in Application.Current.Windows)
                {
                    if(item.Name != "LibraryMainWindow")
                    {
                        item.Close();
                    }
                }


            }
        }

        #region Properties

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
        private NavigationStore navigationStore;

        public string Password

        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        #endregion
    }
}
