using Intermix.Models;
using Intermix.ViewModels;
using Intermix.ViewModels.LibrarySystem;
using Intermix.Views;
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

        public string PIN { get; private set; }

        public MainLibraryPage()
        {
            InitializeComponent();
            DataContext = new MainLibraryPageViewModel();

            using var db = new ApplicationDbContext();

            foreach (var user in db.Users)
            {
                if (user.Username.Equals("Admin"))
                {
                    PIN = user.Password;
                }
            }

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
            Dispatcher.Invoke(() =>
            {
                LibraryMainWindowViewModel.Instance.LoadingVisibility = Visibility.Collapsed;

            });

            BrowseBooksPage page = new();
            NavigationService.Navigate(page);
        }

        private void PswdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            var PswdBox = sender as PasswordBox;
            AdminPage page = new();

            if (PswdBox.Password.Length == 4)
            {
                if (PswdBox.Password.Equals(PIN))
                {
                    NavigationService.Navigate(page);
                }
                else
                {
                    MessageBox.Show("Nieprawidłowe Hasło");
                    PswdBox.Password = String.Empty;
                }
            }


        }
    }
}
