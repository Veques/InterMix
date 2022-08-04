using Intermix.Commands;
using Intermix.Enums;
using Intermix.Models;
using Intermix.ViewModels.Base;
using Intermix.ViewModels.LoginPage;
using Intermix.Views;
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

    #region Models

    public class ReserveBook
    {
        public bool IsChecked { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime PossibleLoanDate { get; set; }

    }

    #endregion

    public class ReservationsPageViewModel : BaseViewModel
    {
        public ICommand ReserveCommand { get; set; }
        

        public ReservationsPageViewModel()
        {
            ReserveCommand = new RelayCommand(
                o => AddReservation(),
                o => ReservedBooks.Any(x => x.IsChecked == true));

            FillDataGrid();
        }

        private void FillDataGrid()
        {
            ReservedBooks = new();
            using var db = new ApplicationDbContext();


            foreach (var book in db.Books.Where(x => x.IsAvailable == 0))
            {
                foreach(var loan in db.Loans.Where(x => x.UserId != LoginPageViewModel.UserId && x.BookId == book.Id))
                {

                    if (db.Reservations.Any(x => x.BookId == book.Id))
                        continue;

                    ReservedBooks.Add(new ReserveBook
                    {
                        IsChecked = false,
                        Id = book.Id,
                        Title = $"{book.Title}",
                        Author = $"{book.AuthorName} {book.AuthorSurname}",
                        ExpectedReturnDate = loan.ExpectedReturn,
                        PossibleLoanDate = loan.ExpectedReturn.AddDays(1)

                    });
                }
            }


            ReservedBooksCollectionView = CollectionViewSource.GetDefaultView(ReservedBooks);
            ReservedBooksCollectionView.Filter = Filter;
        }
        private bool Filter(object obj)
        {
            if (obj is ReserveBook book)
            {
                string Fullname = $"{book.Author}";

                return book.Title.Contains(TitleFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    Fullname.Contains(AuthorFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    book.ExpectedReturnDate.ToString().Contains(ExpectedReturn, StringComparison.InvariantCultureIgnoreCase) &&
                    book.PossibleLoanDate.ToString().Contains(PossibleLoan, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;


        }


        private void AddReservation()
        {
            using var db = new ApplicationDbContext();
            
            foreach (var element in ReservedBooks.Where(x => x.IsChecked))
            {
                db.Reservations.Add(new Reservations
                {
                     UserId = LoginPageViewModel.UserId,
                     BookId = element.Id,
                     ExpectedReturn = element.ExpectedReturnDate

                });

                db.Books.FirstOrDefault(x => x.Id == element.Id).IsReserved = 1;
            }


            if(db.SaveChanges() > 0)
            {
                _ = new CustomizedMessageBox("Saved!", MessageType.Success, MessageButton.Ok).ShowDialog();
                ReservedBooks.Clear();
                FillDataGrid();
            }
            else
            {
                _ = new CustomizedMessageBox("Error", MessageType.Error, MessageButton.Ok).ShowDialog();
            }



        }

        #region Properties

        private ICollectionView _reservedBooksCollectionView;

        public ICollectionView ReservedBooksCollectionView
        {
            get { return _reservedBooksCollectionView; }
            set { _reservedBooksCollectionView = value;
                OnPropertyChanged("ReservedBooksCollectionView");
            }
        }


        private ObservableCollection<ReserveBook> _reservedBooks;

        public ObservableCollection<ReserveBook> ReservedBooks
        {
            get { return _reservedBooks; }
            set { _reservedBooks = value; }
        }


        private string _titleFilter = string.Empty;

        public string TitleFilter
        {
            get { return _titleFilter; }
            set
            {
                _titleFilter = value != null ? _titleFilter = value : _titleFilter = string.Empty;
                OnPropertyChanged("TitleFilter");
                ReservedBooksCollectionView.Refresh();
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
                ReservedBooksCollectionView.Refresh();
            }
        }

        private string _expectedReturn= string.Empty;

        public string ExpectedReturn
        {
            get { return _expectedReturn; }
            set
            {
                _expectedReturn = value != null ? _expectedReturn = value : _expectedReturn = string.Empty;
                OnPropertyChanged("ExpectedReturn");
                ReservedBooksCollectionView.Refresh();
            }
        }

        private string _possibleLoan = string.Empty;

        public string PossibleLoan
        {
            get { return _possibleLoan; }
            set
            {
                _possibleLoan = value != null ? _possibleLoan = value : _possibleLoan = string.Empty;
                OnPropertyChanged("PossibleLoan");
                ReservedBooksCollectionView.Refresh();
            }
        }




        #endregion

    }
}
