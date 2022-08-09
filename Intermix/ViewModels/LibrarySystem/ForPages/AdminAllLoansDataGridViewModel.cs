using Intermix.Commands;
using Intermix.Models;
using Intermix.Models.ModelsForDataGrids;
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
    public class AdminAllLoansDataGridViewModel : BaseViewModel
    {
        public ICommand ClearFilterCommand { get; set; }

        #region Constructor
        public AdminAllLoansDataGridViewModel()
        {
            AllLoans = new();
            Users = new();
            ClearFilterCommand = new RelayCommand(o => ClearFilter(), o => !SelectedUser.Equals(String.Empty));

            FillDataGrid();

            LoansCollectionView = CollectionViewSource.GetDefaultView(AllLoans);
            LoansCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Loan.Username)));
            LoansCollectionView.Filter = Filter;
        }

        private void ClearFilter()
        {
            SelectedIndex = -1;
            SelectedUser = String.Empty;
        }
        #endregion

        #region Fill Data Grid
        private void FillDataGrid()
        {
            using var db = new ApplicationDbContext();

            foreach (var loan in db.Loans)
            {
                foreach (var book in db.Books.Where(x => x.Id == loan.BookId))
                {
                    foreach (var user in db.Users.Where(x => x.Id == loan.UserId))
                    {
                        var status = String.Empty;

                        if (book.IsReserved == 1)
                        {
                            status = $"Reserved by {user.Name} {user.Surname}";
                        }
                        else
                        {
                            status= "Not reserved";
                        }

                        if (!Users.Contains($"{user.Name} {user.Surname}"))
                        {
                            Users.Add($"{user.Name} {user.Surname}");
                        }

                        AllLoans.Add(new Loan
                        {
                            Username = $"{user.Name} {user.Surname}",
                            Title = book.Title,
                            Author = $"{book.AuthorName} {book.AuthorSurname}",
                            LoanDate = loan.LoanDate,
                            ExpectedReturnDate = loan.ExpectedReturn,
                            Status = status
                        });
                    }
                }
            }
        }
        #endregion

        #region Filter
        public bool Filter(object obj)
        {
            if (obj == null)
                return false;

            if(obj is Loan loan)
            {
                return loan.Title.Contains(TitleFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    loan.Author.Contains(AuthorFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    loan.LoanDate.ToString().Contains(PublishDateFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    loan.ExpectedReturnDate.ToString().Contains(ReturnDateFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    loan.Status.Contains(StatusFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    loan.Username.Contains(SelectedUser, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
        #endregion

        #region Properties

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

        private ObservableCollection<string> _users;

        public ObservableCollection<string> Users
        {
            get { return _users; }
            set { _users = value;
                OnPropertyChanged("Users");
            }
        }

        private string _selectedUser = string.Empty;
        public string SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value != null ? _selectedUser = value : _selectedUser = string.Empty;
                OnPropertyChanged("SelectedUser");
                LoansCollectionView.Refresh();
            }
        }

        private int _selectedIndex = -1;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }


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

        private string _statusFilter = string.Empty;
        public string StatusFilter
        {
            get { return _statusFilter; }
            set
            {
                _statusFilter = value != null ? _statusFilter = value : _statusFilter = string.Empty;

                OnPropertyChanged("ReservedFilter");
                LoansCollectionView.Refresh();
            }
        }


        #endregion
    }
}
