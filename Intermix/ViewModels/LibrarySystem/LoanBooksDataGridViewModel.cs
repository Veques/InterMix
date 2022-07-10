using Intermix.Models;
using Intermix.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermix.ViewModels.LibrarySystem
{
    public class LoanBooksModel
    {
        public bool isChecked { get; set; }
        public string? Title { get; set; }
        public string? FullName { get; set; }
    }
    public class LoanBooksDataGridViewModel : BaseViewModel
    {

        public LoanBooksDataGridViewModel()
        {
            using var db = new ApplicationDbContext();
            
            LoanBooks = new();
            foreach (var book in db.Books)
            {
                LoanBooks.Add(new LoanBooksModel 
                { 
                    isChecked = false, 
                    FullName = $"{book.AuthorName} {book.AuthorSurname}", 
                    Title = book.Title 
                });
            }
        }


        private ObservableCollection<LoanBooksModel> _loanBooks;
        public ObservableCollection<LoanBooksModel> LoanBooks
        {
            get { return _loanBooks; }
            set { _loanBooks = value;
                OnPropertyChanged("LoanBooks");
            }
        }

    }
}
