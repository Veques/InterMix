using Intermix.Stores;
using Intermix.ViewModels;
using Intermix.ViewModels.LoginPage;
using System;
using System.Windows;
using System.Windows.Input;

namespace Intermix.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            NavigationStore _navigationStore = new();
            _navigationStore.CurrentViewModel = new LoginPageViewModel(_navigationStore);
            DataContext = new LoginWindowViewModel(_navigationStore);

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (Exception) { }
        }
    }
}
