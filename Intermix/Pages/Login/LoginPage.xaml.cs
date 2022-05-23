using Intermix.ViewModels.LoginPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PasswordBox.Clear();
        }

        private void PasswordBox_MouseMove(object sender, MouseEventArgs e)
        {
            var vm = (LoginPageViewModel)DataContext;
            vm.Password = PasswordBox.Password;
        }
    }
}
