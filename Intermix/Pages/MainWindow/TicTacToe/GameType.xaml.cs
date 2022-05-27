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

namespace Intermix.Pages.MainWindow.TicTacToe
{
    /// <summary>
    /// Interaction logic for GameType.xaml
    /// </summary>
    public partial class GameType : Page
    {
        public GameType()
        {
            InitializeComponent();
            DataContext = new GameTypeViewModel();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ChooseActivity page = new();
            NavigationService.Navigate(page);

        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            GamePage page = new();
            NavigationService.Navigate(page);

        }

        private void IsDifficultySellectableNo_Click(object sender, RoutedEventArgs e)
        {
            Easy.IsChecked = false;
            Hard.IsChecked = false;

            var vm = (GameTypeViewModel) DataContext;
            vm.IsSelectableDifficulty = false;

            vm.IsStartClickable = true;
        }

        private void IsDifficultySellectableYes_Click(object sender, RoutedEventArgs e)
        {

            var vm = (GameTypeViewModel)DataContext;
            vm.IsSelectableDifficulty = true;

            Easy.IsChecked = true;
            vm.IsStartClickable = true;
        }
    }
}
