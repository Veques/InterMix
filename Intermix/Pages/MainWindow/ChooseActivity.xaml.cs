using Intermix.Pages.MainWindow.CurrencyConverter;
using Intermix.Pages.MainWindow.TicTacToe;
using Intermix.ViewModels.MainWindowView.TicTacToeView;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

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
            var item = sender as ListViewItem;
            if (item == null)
                return;

            switch(item.Name)
            {
                case "TicTacToe":

                    GameType page = new();
                    NavigationService.Navigate(page);
                    break;

                case "CurrencyConverter":

                    CurrencyConverterPage currencyConverter = new();
                    NavigationService.Navigate(currencyConverter);
                    break;

            }

        }

    }
}
