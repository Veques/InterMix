using Intermix.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace Intermix.Views
{
    /// <summary>
    /// Interaction logic for RateWindow.xaml
    /// </summary>
    public partial class RateWindow : Window
    {
        public RateWindow()
        {
            InitializeComponent();
            DataContext = new RateWindowViewModel();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (Exception) { }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
