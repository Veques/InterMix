using Intermix.Commands;
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
using System.Windows.Input;

namespace Intermix.ViewModels.LibrarySystem.ForPages
{

    #region Model
    public class Loan
    {
        public string Username { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
    }
    #endregion
    public class AdminAllLoansDataGridViewModel : BaseViewModel
    {

        #region Constructor
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

            LoansCollectionView.Filter = Filter;
        }
        #endregion

        public bool Filter(object obj)
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

        #region Properties

        private string _titleFilter = string.Empty;
        public string TitleFilter
        {
            get { return _titleFilter; }
            set {
                _titleFilter = value != null ? _titleFilter = value : _titleFilter = string.Empty;

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
                _authorFilter = value != null ? _authorFilter = value : _authorFilter = string.Empty;

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
                _publishDateFilter = value != null ? _publishDateFilter = value : _publishDateFilter = string.Empty;

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
                _returnDateFilter = value != null ? _returnDateFilter = value : _returnDateFilter = string.Empty;

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

        #endregion
    }
}
