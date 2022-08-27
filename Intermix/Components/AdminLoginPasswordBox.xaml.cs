using System.Windows;
using System.Windows.Controls;

namespace Intermix.Components
{
    /// <summary>
    /// Interaction logic for AdminLoginPasswordBox.xaml
    /// </summary>
    public partial class AdminLoginPasswordBox : UserControl
    {
        private bool _isPasswordChanging;
        public AdminLoginPasswordBox()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(AdminLoginPasswordBox),
                new PropertyMetadata(string.Empty, PasswordPropertyChanged));

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AdminLoginPasswordBox passwordBox)
            {
                passwordBox.UpdatePassword();
            }
        }

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }


        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _isPasswordChanging = true;
            Password = adminPasswordBox.Password;
            _isPasswordChanging = false;
        }

        private void UpdatePassword()
        {
            if (!_isPasswordChanging)
            {
                adminPasswordBox.Password = Password;
            }
        }
    }
}
