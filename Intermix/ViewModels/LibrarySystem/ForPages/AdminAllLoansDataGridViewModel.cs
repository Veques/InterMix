using Intermix.Models;
using Intermix.ViewModels.Base;
using Intermix.ViewModels.LoginPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Intermix.ViewModels.LibrarySystem.ForPages
{
    public class Loan
    {
        public string Username { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
    }

    public class AdminAllLoansDataGridViewModel : BaseViewModel
    {
        public AdminAllLoansDataGridViewModel()
        {
            AllLoans = new();

            using var db = new ApplicationDbContext();

            foreach (var loan in db.Loans)
            {
                foreach (var book in db.Books.Where(x => x.Id == loan.BookId))
                {
                    foreach (var user in db.Users.Where(x => x.Id == loan.UserId))
                    {
                        AllLoans.Add(new Loan
                        {
                            Username = $"{user.Name} {user.Surname}",
                            Title = book.Title,
                            Author = $"{book.AuthorName} {book.AuthorSurname}",
                            LoanDate = loan.LoanDate,
                            ExpectedReturnDate = loan.ExpectedReturn
                        });
                    }
                }
            }
            LoansCollectionView = CollectionViewSource.GetDefaultView(AllLoans);
            LoansCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Loan.Username)));

            LoansCollectionView.Filter = FilterByTitle;
        }
        public bool FilterByTitle(object obj)
        {
            if (obj == null)
                return false;

            if(obj is Loan loan)
            {
                return loan.Title.Contains(TitleFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    loan.Author.Contains(AuthorFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    loan.LoanDate.ToString().Contains(PublishDateFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    loan.ExpectedReturnDate.ToString().Contains(ReturnDateFilter, StringComparison.InvariantCultureIgnoreCase); 
            }
            return false;
        } 

        private string _titleFilter = string.Empty;
        public string TitleFilter
        {
            get { return _titleFilter; }
            set { 
                if (value != null)
                {
                    _titleFilter = value;
                }
                else
                {
                    _titleFilter = String.Empty;
                }

                OnPropertyChanged("TitleFilter");
                LoansCollectionView.Refresh();
            }
        }

        private string _authorFilter = string.Empty;
        public string AuthorFilter
        {
            get { return _authorFilter; }
            set
            {
                if (value != null)
                {
                    _authorFilter = value;
                }
                else
                {
                    _authorFilter = String.Empty;
                }

                OnPropertyChanged("AuthorFilter");
                LoansCollectionView.Refresh();
            }
        }

        private string _publishDateFilter = string.Empty;
        public string PublishDateFilter
        {
            get { return _publishDateFilter; }
            set
            {
                if (value != null)
                {
                    _publishDateFilter = value;
                }
                else
                {
                    _publishDateFilter = String.Empty;
                }

                OnPropertyChanged("PublishDateFilter");
                LoansCollectionView.Refresh();
            }
        }

        private string _returnDateFilter = string.Empty;
        public string ReturnDateFilter
        {
            get { return _returnDateFilter; }
            set
            {
                if (value != null)
                {
                    _returnDateFilter = value;
                }
                else
                {
                    _returnDateFilter = String.Empty;
                }

                OnPropertyChanged("ReturnDateFilter");
                LoansCollectionView.Refresh();
            }
        }


        private ICollectionView _loanCollectionView;
        public ICollectionView LoansCollectionView
        {
            get { return _loanCollectionView; }
            set { _loanCollectionView = value;
                OnPropertyChanged("LoansCollectionView");
            }
        }


        private ObservableCollection<Loan> _allLoans;
        public ObservableCollection<Loan> AllLoans
        {
            get { return _allLoans; }
            set { _allLoans = value;
                OnPropertyChanged("AllLoans");
            }
        }

    }
}
