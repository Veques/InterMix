using System.Text.RegularExpressions;
using System.Windows.Controls;

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
