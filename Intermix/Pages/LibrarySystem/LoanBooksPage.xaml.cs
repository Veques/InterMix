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
    /// Interaction logic for LoanBooksPage.xaml
    /// </summary>
    public partial class LoanBooksPage : Page
    {
        public LoanBooksPage()
        {
            InitializeComponent();
            DataContext = new LoanBooksPageViewModel();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            var vm = (LoanBooksPageViewModel)DataContext;

            switch (textBox?.Name)
            {
                case "Title":
                    {
                        if (textBox.Text != "")
                        {
                            var filtered = vm.LoanBooks.Where(x => x.Title.ToLower().Contains(textBox.Text.ToLower()));
                            Grid.ItemsSource = null;
                            Grid.ItemsSource = filtered;
                        }
                        else
                        {
                            Grid.ItemsSource = vm.LoanBooks;
                        }
                    }
                    break;
                case "Author":
                    {
                        if (textBox.Text != "")
                        {
                            var filtered = vm.LoanBooks.Where(x => x.FullName.ToLower().Contains(textBox.Text.ToLower()));
                            Grid.ItemsSource = null;
                            Grid.ItemsSource = filtered;
                        }
                        else
                        {
                            Grid.ItemsSource = vm.LoanBooks;
                        }
                    }
                    break;
                case "PublishDate":
                    {
                        if (textBox.Text != "")
                        {
                            var filtered = vm.LoanBooks.Where(x => x.PublishDate.ToString().Contains(textBox.Text));
                            Grid.ItemsSource = null;
                            Grid.ItemsSource = filtered;
                        }
                        else
                        {
                            Grid.ItemsSource = vm.LoanBooks;
                        }
                    }
                    break;
                case "Publisher":
                    {
                        if (textBox.Text != "")
                        {
                            var filtered = vm.LoanBooks.Where(x => x.Publisher.ToString().Contains(textBox.Text.ToLower()));
                            Grid.ItemsSource = null;
                            Grid.ItemsSource = filtered;
                        }
                        else
                        {
                            Grid.ItemsSource = vm.LoanBooks;
                        }
                    }
                    break;
            }
        }
    }
}
