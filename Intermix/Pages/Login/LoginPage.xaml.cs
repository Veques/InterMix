using Intermix.ViewModels.LoginPage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Intermix.Pages.Login
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            DataContext = new LoginPageViewModel();
        }

        private void NewAccount_Click(object sender, RoutedEventArgs e)
        {
            CreateAccountPage page = new();

            NavigationService.Navigate(page);
        }

    }
}
