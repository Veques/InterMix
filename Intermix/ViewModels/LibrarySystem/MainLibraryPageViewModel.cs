using Intermix.Models;
using Intermix.ViewModels.Base;
using Intermix.ViewModels.LoginPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermix.ViewModels.LibrarySystem
{   
    public class PrivateLoans
    {
        public string Author { get; set; }
        public string Title { get; set; }
    }

 
    public class MainLibraryPageViewModel : BaseViewModel
    {

        public MainLibraryPageViewModel()
        {
            LoggedUser = LoginPageViewModel.User;
            UserId = LoginPageViewModel.UserId;

            LoanedBooks = new();

            using var db = new ApplicationDbContext();

            foreach (var loanElement in db.Loans.Where(x => x.UserId == UserId))
            {

                foreach(var bookElement in db.Books.Where(x => x.Id == loanElement.BookId))
                {
                    LoanedBooks.Add(new PrivateLoans
                    {
                        Author = $"{bookElement.AuthorName} {bookElement.AuthorSurname}",
                        Title = bookElement.Title,
                        
                    });

                }

            }

        }


        private ObservableCollection<PrivateLoans> _loanedBooks;

        public ObservableCollection<PrivateLoans> LoanedBooks
        {
            get { return _loanedBooks; }
            set { _loanedBooks = value;
                OnPropertyChanged("LoanedBooks");
            }
        }

        public int UserId { get; private set; }

        private string _loggedUser;

        public string LoggedUser
        {
            get { return _loggedUser; }
            set { _loggedUser = value;
                OnPropertyChanged("LoggedUser");
            }
        }

    }
}
