using System.Windows;
using System.Windows.Controls;

namespace Intermix.Pages.LibrarySystem
{
    /// <summary>
    /// Interaction logic for BrowseBooksPage.xaml
    /// </summary>
    public partial class BrowseBooksPage : Page
    {
        public BrowseBooksPage()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
