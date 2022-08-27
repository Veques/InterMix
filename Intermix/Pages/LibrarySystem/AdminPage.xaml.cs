using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Intermix.Pages.LibrarySystem
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var ue = e.Source as TextBox;
            Regex regex;

            regex = new Regex("[^0-9]+");

            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
