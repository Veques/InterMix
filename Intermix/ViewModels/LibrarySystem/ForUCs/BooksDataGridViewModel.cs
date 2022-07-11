using Intermix.Models;
using Intermix.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermix.ViewModels.LibrarySystem.ForUCs
{
    public class BooksDataGridViewModel : BaseViewModel
    {

        public BooksDataGridViewModel()
        {
            using (var db = new ApplicationDbContext())
            {
                AllBooks = new(db.Books);
            }
        }


        private ObservableCollection<Books> _allBooks;
        public ObservableCollection<Books> AllBooks
        {
            get { return _allBooks; }
            set
            {
                _allBooks = value;
                OnPropertyChanged("AllBooks");
            }
        }

    }
}
