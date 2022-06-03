using Intermix.Pages.MainWindow.TicTacToe;
using Intermix.ViewModels.MainWindowView.TicTacToeView;
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

namespace Intermix.Pages.MainWindow
{
    /// <summary>
    /// Interaction logic for ChooseActivity.xaml
    /// </summary>
    public partial class ChooseActivity : Page
    {
        public ChooseActivity()
        {
            InitializeComponent();
            DataContext = new ChooseActivityViewModel();
        }
        private void ListViewItem_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

            GameType page = new();
            NavigationService.Navigate(page);
        }
    }
}
