using Intermix.ViewModels.LibrarySystem.ForPages;
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
    /// Interaction logic for ReturnBooksPage.xaml
    /// </summary>
    public partial class ReturnBooksPage : Page
    {
        public ReturnBooksPage()
        {
            InitializeComponent();
            DataContext = new ReturnBooksPageViewModel();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            var vm = (ReturnBooksPageViewModel)DataContext;

            switch (textBox?.Name)
            {
                case "Title":
                    {
                        if (textBox.Text != "")
                        {
                            var filtered = vm.ReturnBooks.Where(x => x.Title.ToLower().Contains(textBox.Text.ToLower()));
                            Grid.ItemsSource = null;
                            Grid.ItemsSource = filtered;
                        }
                        else
                        {
                            Grid.ItemsSource = vm.ReturnBooks;
                        }
                    }
                    break;
                case "Author":
                    {
                        if (textBox.Text != "")
                        {
                            var filtered = vm.ReturnBooks.Where(x => x.FullName.ToLower().Contains(textBox.Text.ToLower()));
                            Grid.ItemsSource = null;
                            Grid.ItemsSource = filtered;
                        }
                        else
                        {
                            Grid.ItemsSource = vm.ReturnBooks;
                        }
                    }
                    break;
                case "LoanDate":
                    {
                        if (textBox.Text != "")
                        {
                            var filtered = vm.ReturnBooks.Where(x => x.LoanDate.ToString().Contains(textBox.Text));
                            Grid.ItemsSource = null;
                            Grid.ItemsSource = filtered;
                        }
                        else
                        {
                            Grid.ItemsSource = vm.ReturnBooks;
                        }
                    }
                    break;
                case "ExpectedReturn":
                    {
                        if (textBox.Text != "")
                        {
                            var filtered = vm.ReturnBooks.Where(x => x.ReturnDate.ToString().Contains(textBox.Text));
                            Grid.ItemsSource = null;
                            Grid.ItemsSource = filtered;
                        }
                        else
                        {
                            Grid.ItemsSource = vm.ReturnBooks;
                        }
                    }
                    break;
            }
        }

    }
}
