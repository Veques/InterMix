using Intermix.Enums;
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
using System.Windows.Shapes;

namespace Intermix.Views
{
    /// <summary>
    /// Interaction logic for CustomizedMessageBox.xaml
    /// </summary>
    public partial class CustomizedMessageBox : Window
    {
        public CustomizedMessageBox(string Message, MessageType Type, MessageButton Buttons)
        {
            InitializeComponent();
            txtMessage.Text = Message;

            switch (Type)
            {

                case MessageType.Information:
                        txtTitle.Text = "Information";
                        cardHeader.Background = Brushes.DeepSkyBlue;
                    break;

                case MessageType.Confirmation:
                        txtTitle.Text = "Confirmation";
                        cardHeader.Background = Brushes.DarkBlue;   
                    break;

                case MessageType.Success:
                        txtTitle.Text = "Success";
                        cardHeader.Background = Brushes.LawnGreen;
                    break;

                case MessageType.Warning:
                        txtTitle.Text = "Warning";
                        cardHeader.Background = Brushes.Orange;
                    break;

                case MessageType.Error:
                        txtTitle.Text = "Error";
                        cardHeader.Background = Brushes.Red;
                    break;
            }
            switch (Buttons)
            {
                case MessageButton.OkCancel:
                    btnYes.Visibility = Visibility.Collapsed; btnNo.Visibility = Visibility.Collapsed;
                    break;

                case MessageButton.YesNo:
                    btnOk.Visibility = Visibility.Collapsed; btnCancel.Visibility = Visibility.Collapsed;
                    break;

                case MessageButton.Ok:
                    btnOk.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Collapsed; btnNo.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (Exception) { }
        }
    }
}

