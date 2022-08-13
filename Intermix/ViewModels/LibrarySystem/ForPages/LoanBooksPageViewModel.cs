using Intermix.Commands;
using Intermix.Enums;
using Intermix.Models;
using Intermix.Models.LibrarySystemModels;
using Intermix.Stores;
using Intermix.ViewModels.Base;
using Intermix.ViewModels.LoginPage;
using Intermix.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace Intermix.ViewModels.LibrarySystem.ForPages
{

    public class LoanBooksPageViewModel : BaseViewModel
    {
        public ICommand LoanCommand { get; set; }
        public ICommand BackMainCommand { get; set; }

        public LoanBooksPageViewModel(NavigationStore navigationStore)
        {

            LoanCommand = new RelayCommand(
                o => LoanClicked(),
                o => LoanBooks.Any(x => x.IsChecked == true)
                );

            BackMainCommand = new NavigationCommand<MainLibraryPageViewModel>(navigationStore,
                () => new MainLibraryPageViewModel(navigationStore),
                x => true);
            
            LoanBooks = new();
            FillDataGrid();
        }

        private bool Filter(object obj)
        {
            if (obj is LoanBook book)
            {
                return book.Title.Contains(TitleFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    book.FullName.Contains(AuthorFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    book.PublishDate.ToString().Contains(PublishDateFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    book.Publisher.Contains(PublisherFilter, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        private void FillDataGrid()
        {            
            using var db = new ApplicationDbContext();
            foreach (var book in db.Books.Where(d => d.IsAvailable == 1))
            {
                LoanBooks.Add(new LoanBook
                {
                    IsChecked = false,
                    Id = book.Id,
                    FullName = $"{book.AuthorName} {book.AuthorSurname}",
                    Title = book.Title,
                    PublishDate = book.PublishDate,
                    Publisher = book.Publisher
                });
            }
                LoanBooksCollectionView = CollectionViewSource.GetDefaultView(LoanBooks);
                LoanBooksCollectionView.Filter = Filter;
        }
        private void LoanClicked()
        {
            bool? result = new CustomizedMessageBox("Do you really want to borrow these books?", MessageType.Confirmation, MessageButton.OkCancel)
                .ShowDialog();

            if (!result.Value)
                return;

            using ApplicationDbContext db = new();
            foreach (var book in LoanBooks.Where(a => a.IsChecked == true))
            {
                db.Loans.Add(new Loans
                {
                    UserId = LoginPageViewModel.UserId,
                    BookId = book.Id,
                    LoanDate = DateTime.Now.Date,
                    ExpectedReturn = DateTime.Now.Date.AddDays(14),
                    WasExtended = 0
                });

                var cell = db.Books.FirstOrDefault(x => x.Id == book.Id);

                if (cell == null)
                    continue;
                    
                cell.IsAvailable = 0;
            }

            if(db.SaveChanges() > 0)
            {
                _ = new CustomizedMessageBox("Loan successful", MessageType.Success, MessageButton.Ok).ShowDialog();
                LoanBooks.Clear();
                FillDataGrid();
            }
            else
            {
                _ = new CustomizedMessageBox("Something went wrong", MessageType.Error, MessageButton.Ok).ShowDialog();
            }

        }

        #region Properties

        private ICollectionView _loanBooksCollectionView;
        public ICollectionView LoanBooksCollectionView
        {
            get { return _loanBooksCollectionView; }
            set { _loanBooksCollectionView = value;
                OnPropertyChanged("LoanBooksCollectionView");
            }
        }

        private ObservableCollection<LoanBook> _loanBooks;
        public ObservableCollection<LoanBook> LoanBooks
        {
            get { return _loanBooks; }
            set
            {
                _loanBooks = value;
                OnPropertyChanged("LoanBooks");
            }
        }

        private string _titleFilter = string.Empty;

        public string TitleFilter
        {
            get { return _titleFilter; }
            set
            {
                _titleFilter = value != null ? _titleFilter = value : _titleFilter = string.Empty;
                OnPropertyChanged("TitleFilter");
                LoanBooksCollectionView.Refresh();
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
                LoanBooksCollectionView.Refresh();
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
                LoanBooksCollectionView.Refresh();
            }
        }

        private string _publisherFilter = string.Empty;
        public string PublisherFilter
        {
            get { return _publisherFilter; }
            set
            {
                _publisherFilter = value != null ? _publisherFilter = value : _publisherFilter = string.Empty;
                OnPropertyChanged("PublisherFilter");
                LoanBooksCollectionView.Refresh();
            }
        }
        #endregion
    }
}
