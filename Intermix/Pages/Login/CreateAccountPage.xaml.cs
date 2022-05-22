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
    }
}
