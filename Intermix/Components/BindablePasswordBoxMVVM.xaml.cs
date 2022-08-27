using System.Windows;
using System.Windows.Controls;

namespace Intermix.Components
{
    /// <summary>
    /// Interaction logic for BindablePasswordBoxMVVM.xaml
    /// </summary>
    public partial class BindablePasswordBoxMVVM : UserControl
    {
        private bool _isPasswordChanging;

        public BindablePasswordBoxMVVM()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBoxMVVM),
                new PropertyMetadata(string.Empty, PasswordPropertyChanged));

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BindablePasswordBoxMVVM passwordBox)
            {
                passwordBox.UpdatePassword();
            }
        }

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }


        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _isPasswordChanging = true;
            Password = passwordBox.Password;
            _isPasswordChanging = false;
        }

        private void UpdatePassword()
        {
            if (!_isPasswordChanging)
            {
                passwordBox.Password = Password;
            }
        }
    }
}
