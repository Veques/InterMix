using Intermix.ViewModels.LibrarySystem.ForUCs;
using System.Linq;
using System.Windows.Controls;

namespace Intermix.UserControls
{
    /// <summary>
    /// Interaction logic for BooksDataGridUserControl.xaml
    /// </summary>
    public partial class BooksDataGridUserControl : UserControl
    {
        public BooksDataGridUserControl()
        {
            InitializeComponent();
            DataContext = new BooksDataGridViewModel();
        }

        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            var vm = (BooksDataGridViewModel)DataContext;


            switch (textBox?.Name)
            {
                case "Title":
                    {
                        if (textBox.Text != "")
                        {
                            var filtered = vm.AllBooks.Where(x => x.Title.ToLower().Contains(textBox.Text.ToLower()));
                            Grid.ItemsSource = null;
                            Grid.ItemsSource = filtered;
                        }
                        else
                        {
                            Grid.ItemsSource = vm.AllBooks;
                        }
                    }
                    break;
                case "Author":
                    {
                    if (textBox.Text != "")
                    {
                        var filtered = vm.AllBooks.Where(x => x.AuthorName.ToLower().Contains(textBox.Text.ToLower()));
                        Grid.ItemsSource = null;
                        Grid.ItemsSource = filtered;
                    }
                    else
                    {
                        Grid.ItemsSource = vm.AllBooks;
                    }
                }
                    break;
                case "PublishDate":
                    {
                    if (textBox.Text != "")
                    {
                        var filtered = vm.AllBooks.Where(x => x.PublishDate.ToString().Contains(textBox.Text));
                        Grid.ItemsSource = null;
                        Grid.ItemsSource = filtered;
                    }
                    else
                    {
                        Grid.ItemsSource = vm.AllBooks;
                    }
                }
                    break;
                case "Publisher":
                    {
                        if (textBox.Text != "")
                        {
                            var filtered = vm.AllBooks.Where(x => x.Publisher.ToLower().Contains(textBox.Text.ToLower()));
                            Grid.ItemsSource = null;
                            Grid.ItemsSource = filtered;
                        }
                        else
                        {
                            Grid.ItemsSource = vm.AllBooks;
                        }
                    }
                    break;
            }

        }
    }
}
