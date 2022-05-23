using Intermix.ViewModels.LoginPage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Intermix.Pages.Login
{
    /// <summary>
    /// Interaction logic for CreateAccountPage.xaml
    /// </summary>
    public partial class CreateAccountPage : Page
    {
        public CreateAccountPage()
        {
            InitializeComponent();
            DataContext = new CreateAccountViewModel();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var vm = (CreateAccountViewModel)DataContext;
            vm.Password = PasswordBox.Password;
        }

        private void PasswordBoxConfirm_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var vm = (CreateAccountViewModel)DataContext;
            vm.PasswordConfirm = PasswordBoxConfirm.Password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PasswordBox.Clear();
            PasswordBoxConfirm.Clear();
        }
    }
}
