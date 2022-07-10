using Intermix.ViewModels.LibrarySystem;
using System.Windows.Controls;

namespace Intermix.UserControls
{
    /// <summary>
    /// Interaction logic for LoanBooksDataGridUserControl.xaml
    /// </summary>
    public partial class LoanBooksDataGridUserControl : UserControl
    {
        public LoanBooksDataGridUserControl()
        {
            InitializeComponent();
            DataContext = new LoanBooksDataGridViewModel();
        }
    }
}
