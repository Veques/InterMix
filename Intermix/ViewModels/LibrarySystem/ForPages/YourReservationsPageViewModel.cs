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
    public class YourReservationsPageViewModel : BaseViewModel
    {

        #region Models
        public class Reservation
        {
            public bool IsChecked { get; set; }
            public bool IsEnabled { get; set; }
            public int Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public DateTime OpenToLoan { get; set; }

        }

        #endregion

        public ICommand LoanCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public YourReservationsPageViewModel()
        {
            LoanCommand = new RelayCommand(
                o => LoanClicked(),
                o => Reservations.Any(x => x.IsChecked == true)
                );

            CancelCommand = new RelayCommand(CancelClicked, o => true);


            FillDataGrid();
        }
        private void FillDataGrid()
        {
            Reservations = new();
            using var db = new ApplicationDbContext();

            foreach (var reservation in db.Reservations.Where(x => x.UserId == LoginPageViewModel.UserId))
            {
                foreach (var book in db.Books.Where(x => x.Id == reservation.BookId && x.IsReserved == 1))
                {

                    Reservations.Add(new Reservation
                    {
                        IsChecked = false,
                        IsEnabled = DateTime.Now.Date >= reservation.ReturnDate.Date && reservation.ReturnDate != DateTime.MinValue,
                        Id = book.Id,
                        Title = $"{book.Title}",
                        Author = $"{book.AuthorName} {book.AuthorSurname}",
                        OpenToLoan = reservation.ExpectedReturn.Date.AddDays(-16)
                    });
                }
            }

            ReservationsCollectionView = CollectionViewSource.GetDefaultView(Reservations);
            ReservationsCollectionView.Filter = Filter;
        }

        private void LoanClicked()
        {
            bool? result = new CustomizedMessageBox("Do you really want to borrow these books?", MessageType.Confirmation, MessageButton.OkCancel).ShowDialog();

            if (!result.Value)
                return;

            using var db = new ApplicationDbContext();

            foreach(var element in Reservations.Where(x => x.IsChecked == true))
            {
                db.Loans.Add(new Loans
                {
                    UserId = LoginPageViewModel.UserId,
                    BookId = element.Id,
                    ExpectedReturn = DateTime.Now.Date.AddDays(14),
                    LoanDate = DateTime.Now.Date
                });

                db.Reservations.RemoveRange(db.Reservations.Where(x => x.BookId == element.Id));
                var cell = db.Books.FirstOrDefault(x => x.Id == element.Id);

                if (cell == null)
                    continue;

                cell.IsAvailable = 0;

            }

            if(db.SaveChanges() > 0)
            {
                _ = new CustomizedMessageBox("Saved!", MessageType.Success, MessageButton.Ok).ShowDialog();
                Reservations.Clear();
                FillDataGrid();
            }
            else
            {
                _ = new CustomizedMessageBox("Error has occured", MessageType.Error, MessageButton.Ok).ShowDialog();
            }
        }

        private void CancelClicked(Object parameter)
        {
            using var db = new ApplicationDbContext();
            
            if (parameter == null)
                return;

            if (parameter is Reservation reservation)
            {
                db.Reservations.Remove(db.Reservations.Where(x => x.BookId == reservation.Id).FirstOrDefault());
                db.Books.Single(x => x.Id == reservation.Id).IsReserved = 0;

                _ = new CustomizedMessageBox("Are you sure? \n You will not be able to reserve this book again",
                    MessageType.Confirmation, MessageButton.OkCancel).ShowDialog();

                if(db.SaveChanges() > 0)
                {
                    _ = new CustomizedMessageBox("Success!", MessageType.Success, MessageButton.Ok).ShowDialog();
                    Reservations.Clear();
                    FillDataGrid();
                }
            }

        }


        private bool Filter(object obj)
        {
            if (obj is Reservation reservation)
            {
                string Fullname = $"{reservation.Author}";

                return reservation.Title.Contains(TitleFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    Fullname.Contains(AuthorFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    reservation.OpenToLoan.ToString().Contains(OpenToLoanFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        #region Properties 

        private ICollectionView _reservationsCollectionView;
        public ICollectionView ReservationsCollectionView
        {
            get { return _reservationsCollectionView; }
            set { _reservationsCollectionView = value;
                OnPropertyChanged("ReservationsCollectionView");
            }
        }

        private ObservableCollection<Reservation> _reservations;

        public ObservableCollection<Reservation> Reservations
        {
            get { return _reservations; }
            set { _reservations = value; }
        }

        private Reservation _cancelCommandParameter;

        public Reservation CancelCommandParameter
        {
            get { return _cancelCommandParameter; }
            set { _cancelCommandParameter = value;
                OnPropertyChanged("CancelCommandParameter");
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
                ReservationsCollectionView.Refresh();
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
                ReservationsCollectionView.Refresh();
            }
        }


        private string _openToLoanFilter = string.Empty;
        public string OpenToLoanFilter
        {
            get { return _openToLoanFilter; }
            set
            {
                _openToLoanFilter = value != null ? _openToLoanFilter = value : _openToLoanFilter = string.Empty;
                OnPropertyChanged("OpenToLoanFilter");
                ReservationsCollectionView.Refresh();
            }
        }



        #endregion






    }
}