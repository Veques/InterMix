using Intermix.Commands;
using Intermix.Models;
using Intermix.Models.LibrarySystemModels;
using Intermix.Stores;
using Intermix.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Intermix.ViewModels.LibrarySystem.ForPages
{

    public class BrowseBooksPageViewModel : BaseViewModel
    {
        public ICommand BackMainCommand { get; set; }

        #region Constructor
        public BrowseBooksPageViewModel(NavigationStore navigationStore)
        {
            BackMainCommand = new NavigationCommand<MainLibraryPageViewModel>(navigationStore,
                () => new MainLibraryPageViewModel(navigationStore),
                x => true);

            AllBooks = new();

            FillDataGrid();

            BooksCollectionView = CollectionViewSource.GetDefaultView(AllBooks);
            BooksCollectionView.Filter = FilterBooks;

        }

        #endregion

        #region Fill Data Grid
        private void FillDataGrid()
        {
            using var db = new ApplicationDbContext();

            foreach (var element in db.Books)
            {
                string status = String.Empty;
                var statusColor = Brushes.Black;

                if (element.IsAvailable == 1 && element.IsReserved == 0)
                {
                    status = "Available";
                    statusColor = Brushes.DarkSeaGreen;
                }
                if (element.IsAvailable == 0 && element.IsReserved == 0)
                {
                    status = $"Loaned till {db.Loans.Single(x => x.BookId == element.Id).ExpectedReturn.ToShortDateString()}";
                    statusColor = Brushes.Orange;
                }
                if (element.IsAvailable == 0 && element.IsReserved == 1 && db.Reservations.Any())
                {
                    var whenAvailable = db.Reservations.Single(x => x.BookId == element.Id).ReturnDate == DateTime.MinValue ?
                                        db.Reservations.Single(x => x.BookId == element.Id).ExpectedReturn.ToShortDateString() :
                                        db.Reservations.Single(x => x.BookId == element.Id).ReturnDate.AddDays(2).ToShortDateString();

                    status = $"Reserved till {whenAvailable}";
                    statusColor = Brushes.Red;
                }

                
                AllBooks.Add(new Book
                {
                    Id = element.Id,
                    Title = element.Title,
                    Author = $"{element.AuthorName} {element.AuthorSurname}",
                    PublishDate = element.PublishDate,
                    Publisher = element.Publisher,
                    Status = status,
                    StatusColor = statusColor
                });
            }
        }
        #endregion

        #region Filter
        private bool FilterBooks(object obj)
        {
            if (obj is Book book)
            {
                return book.Title.Contains(TitleFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    book.Publisher.Contains(PublisherFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    book.PublishDate.ToString().Contains(PublishDateFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    book.Author.Contains(AuthorFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    book.Status.Contains(StatusFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
        #endregion

        #region Properties

        private ICollectionView _booksCollectionView;
        public ICollectionView BooksCollectionView
        {
            get { return _booksCollectionView; }
            set
            {
                _booksCollectionView = value;
                OnPropertyChanged("BooksCollectionView");
            }
        }

        private ObservableCollection<Book> _allBooks;
        public ObservableCollection<Book> AllBooks
        {
            get { return _allBooks; }
            set
            {
                _allBooks = value;
                OnPropertyChanged("AllBooks");
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
                BooksCollectionView.Refresh();
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
                BooksCollectionView.Refresh();
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
                BooksCollectionView.Refresh();
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
                BooksCollectionView.Refresh();
            }
        }

        private string _statusFilter = string.Empty;
        public string StatusFilter
        {
            get { return _statusFilter; }
            set
            {
                _statusFilter = value != null ? _statusFilter = value : _statusFilter = string.Empty;

                OnPropertyChanged("StatusFilter");
                BooksCollectionView.Refresh();
            }
        }

        #endregion

    }
}
