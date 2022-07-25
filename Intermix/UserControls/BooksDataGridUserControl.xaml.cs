using Intermix.ViewModels.LibrarySystem.ForUCs;
using System.Linq;
using System.Windows.Controls;

namespace Intermix.UserControls
{
    /// <summary>
    /// Interaction logic for BooksDataGridUserControl.xaml
    /// </summary>
    public partial class BooksDataGridUserControl : UserControl
    {
        public BooksDataGridUserControl()
        {
            InitializeComponent();
            DataContext = new BooksDataGridViewModel();
        }

    }
}
