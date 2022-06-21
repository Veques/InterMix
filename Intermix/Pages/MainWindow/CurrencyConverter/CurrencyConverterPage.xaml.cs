using Intermix.ViewModels.CurrencyConverter;
using System.Text.RegularExpressions;
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

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var ue = e.Source as TextBox;
            Regex regex;
            if (ue.Text.Contains("."))
            {
                regex = new Regex("[^0-9]+");
            }
            else
            {
                regex = new Regex("[^0-9.]+");
            }

            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
