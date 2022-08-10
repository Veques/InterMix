using Intermix.Enums;
using Intermix.Stores;
using Intermix.ViewModels;
using Intermix.Views;
using System.Windows;
using System.Windows.Input;

namespace Intermix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            NavigationStore _navigationStore = new();
            _navigationStore.CurrentViewModel = new ChooseActivityViewModel(_navigationStore);
            DataContext = new MainWindowViewModel(_navigationStore);
        }

        private void Drag_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {

            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }

        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings setting = new();
            setting.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RateWindow window = new();
            window.ShowDialog();
        }
    }
}
