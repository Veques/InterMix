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

namespace Intermix.Pages.LibrarySystem
{
    /// <summary>
    /// Interaction logic for MainLibraryPage.xaml
    /// </summary>
    public partial class MainLibraryPage : Page
    {
        public MainLibraryPage()
        {
            InitializeComponent();
        }

        private void LoanBooks_Click(object sender, RoutedEventArgs e)
        {
            LoanBooksPage page = new();
            NavigationService.Navigate(page);       
        }
        private void ReturnBooks_Click(object sender, RoutedEventArgs e)
        {
            ReturnBooksPage page = new();
            NavigationService.Navigate(page);
        }
        private void BrowseBooks_Click(object sender, RoutedEventArgs e)
        {
            BrowseBooksPage page = new();
            NavigationService.Navigate(page);
        }
        private void AdminPage_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Admin
        }
    }
}
