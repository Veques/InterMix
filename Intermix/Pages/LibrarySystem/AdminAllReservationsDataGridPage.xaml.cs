using Intermix.ViewModels.LibrarySystem.ForPages;
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

namespace Intermix.Pages.LibrarySystem
{
    /// <summary>
    /// Interaction logic for AdminAllReservationsDataGridPage.xaml
    /// </summary>
    public partial class AdminAllReservationsDataGridPage : Page
    {
        public AdminAllReservationsDataGridPage()
        {
            InitializeComponent();
            DataContext = new AdminAllReservationsDataGridViewModel();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
