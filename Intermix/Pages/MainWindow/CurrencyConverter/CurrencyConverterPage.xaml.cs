using Intermix.ViewModels.CurrencyConverter;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Intermix.Pages.MainWindow.CurrencyConverter
{
    /// <summary>
    /// Interaction logic for CurrencyConverterPage.xaml
    /// </summary>
    public partial class CurrencyConverterPage : Page
    {
        public CurrencyConverterPage()
        {
            InitializeComponent();
            DataContext = new CurrencyConverterPageViewModel();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
