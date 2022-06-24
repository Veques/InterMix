using Intermix.ViewModels;
using System.Windows.Controls;

namespace Intermix.Pages
{
    /// <summary>
    /// Interaction logic for Info.xaml
    /// </summary>
    public partial class Info : Page
    {
        public Info()
        {
            InitializeComponent();
            DataContext = new InfoViewModel();
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
