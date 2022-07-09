using Intermix.ViewModels;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Intermix.Views
{
    /// <summary>
    /// Interaction logic for LibraryMainWindow.xaml
    /// </summary>
    public partial class LibraryMainWindow : Window
    {
        public LibraryMainWindow()
        {
            InitializeComponent();
            DataContext = new LibraryMainWindowViewModel();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }
    }
}
