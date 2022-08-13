using Intermix.Commands;
using Intermix.Models;
using Intermix.Models.LibrarySystemModels;
using Intermix.Stores;
using Intermix.ViewModels.Base;
using Intermix.ViewModels.LibrarySystem.ForPages;
using Intermix.ViewModels.LoginPage;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Intermix.ViewModels.LibrarySystem
{
    public class MainLibraryPageViewModel : BaseViewModel
    {
        public ICommand ReservationsCommand { get; set; }
        public ICommand YourReservationsCommand { get; set; }
        public ICommand LoanBooksCommand { get; set; }
        public ICommand ReturnBooksCommand { get; set; }
        public ICommand BrowseBooksCommand { get; set; }
        public ICommand AdminCommand { get; set; }


        #region Constructor
        public MainLibraryPageViewModel(NavigationStore navigationStore)
        {

            LoggedUser = LoginPageViewModel.User;
            UserId = LoginPageViewModel.UserId;

            using var db = new ApplicationDbContext();

            LoanedBooks = new();
            FillPrivateLoans();
            ManageDateChanges();

            YourReservationsEnabled = db.Reservations.Any(x => x.UserId == LoginPageViewModel.UserId);
            ManageNewNotifications();

            ReservationsCommand = new NavigationCommand<ReservationsPageViewModel>(navigationStore,
                () => new ReservationsPageViewModel(navigationStore),
                x => true);

            YourReservationsCommand = new NavigationCommand<YourReservationsPageViewModel>(navigationStore,
                () => new YourReservationsPageViewModel(navigationStore),
                x => true);

            LoanBooksCommand = new NavigationCommand<LoanBooksPageViewModel>(navigationStore,
                () => new LoanBooksPageViewModel(navigationStore),
                x => true);

            ReturnBooksCommand = new NavigationCommand<ReturnBooksPageViewModel>(navigationStore,
                () => new ReturnBooksPageViewModel(navigationStore),
                x => true);

            BrowseBooksCommand = new NavigationCommand<BrowseBooksPageViewModel>(navigationStore,
                () => new BrowseBooksPageViewModel(navigationStore),
                x => true);

            AdminCommand = new NavigationCommand<AdminPageViewModel>(navigationStore,
                () => new AdminPageViewModel(navigationStore),
                x => true);


            NotificationsCollectionView = CollectionViewSource.GetDefaultView(Notifications);
            NotificationsCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Notification.NotificationType)));
        }

        #endregion

        private void CheckPassword(string value)
        {
            using var db = new ApplicationDbContext();

            if (value == db.Users.Single(x => x.Username == "Admin").Password)
            {
                AdminCommand.Execute(this);
            }
                Password = String.Empty;
        }

        #region FillProfile
        private void FillPrivateLoans()
        {
            using var db = new ApplicationDbContext();

            foreach (var loanElement in db.Loans.Where(x => x.UserId == UserId))
            {
                foreach (var bookElement in db.Books.Where(x => x.Id == loanElement.BookId))
                {
                    LoanedBooks.Add(new PrivateLoans
                    {
                        Author = $"{bookElement.AuthorName} {bookElement.AuthorSurname}",
                        Title = bookElement.Title,
                        LoanDate = loanElement.LoanDate,
                        ReturnDate = loanElement.ExpectedReturn

                    });
                }
            }
        }
        
        #endregion

        #region Automatic returns to make reservations work 
        private static void ManageDateChanges()
        {
            using var db = new ApplicationDbContext();

            foreach(var element in db.Books)
            {
                foreach (var reservation in db.Reservations.Where(x => x.BookId == element.Id))
                {

                    if(DateTime.Now.Date >= reservation.ExpectedReturn.Date && element.IsReserved == 0)
                    {
                        element.IsAvailable = 1;
                        db.Loans.RemoveRange(db.Loans.Where(x => x.BookId == element.Id));

                        db.SaveChanges();
                    }


                    if (element.IsReserved == 1 && DateTime.Now.Date > reservation.EndOfReservation.Date)
                    {
                        db.Reservations.RemoveRange(db.Reservations.Where(x => x.BookId == element.Id));
                        element.IsReserved = 0;
                        db.SaveChanges();
                    }
                }
            }
        }

        #endregion

        #region Notification

        private void ManageNewNotifications()
        {
            using var db = new ApplicationDbContext();

            Notifications = new();
            DateTime date = DateTime.Now.Date;

            //End of Loan
            foreach (var loan in db.Loans.Where(x => x.UserId == UserId))
            {
                var timeSpan = loan.ExpectedReturn.Subtract(date);

                if (timeSpan.TotalDays <= 5 && timeSpan.TotalDays >= 0)
                {
                    Notifications.Add(new Notification
                    {
                        Title = db.Books.Single(x => x.Id == loan.BookId).Title,
                        AdditionalDescription = $"Return Books before: {loan.ExpectedReturn.Date.ToShortDateString()}",
                        Foreground = Brushes.Red,
                        NotificationType = "End of Loan"
                    });

                    NotificationsCount += 1;
                }
            }

            //End of Reservation
            foreach (var reservation in db.Reservations.Where(x => x.UserId == UserId))
            {
                var timeSpan = reservation.EndOfReservation.Subtract(date);

                if (timeSpan.TotalDays <= 2 && timeSpan.TotalDays >= 0)
                {
                    Notifications.Add(new Notification
                    {
                        Title = db.Books.Single(x => x.Id == reservation.BookId).Title,
                        AdditionalDescription = $"Your reservation expires at: {reservation.EndOfReservation.Date.ToShortDateString()}" ,
                        Foreground = Brushes.BlueViolet,
                        NotificationType = "End of Reservation"
                    });

                    NotificationsCount += 1;
                }
            }

            //Possible Faster Reservation
            foreach (var reservation in db.Reservations.Where(x => x.UserId == UserId))
            {
                DateTime fasterLoan = DateTime.MinValue;

                if (reservation.ReturnDate.Date == DateTime.MinValue.Date)
                {
                    continue;
                }
                else
                {
                    
                    fasterLoan = reservation.ReturnDate.Date;
                }

                if (reservation.EndOfReservation.Date.AddDays(-1) <= DateTime.Now.Date)
                {
                    continue;
                }

                Notifications.Add(new Notification
                {
                    Title = db.Books.Single(x => x.Id == reservation.BookId).Title,
                    Foreground = Brushes.DarkGreen,
                    NotificationType = "Possible faster loan",
                    AdditionalDescription = "Previous user returned the book, you can freely loan it now"
                    
                });

                NotificationsCount += 1;
            }  
        }
        #endregion

        #region Properties

        public static int UserId { get; private set; }
        public static string LoggedUser { get; private set; }


        private int _notificationsCount;

        public int NotificationsCount
        {
            get { return _notificationsCount; }
            set
            {
                _notificationsCount = value;
                OnPropertyChanged("NotificationsCount");
            }
        }

        private ICollectionView _notificationsCollectionView;

        public ICollectionView NotificationsCollectionView
        {
            get { return _notificationsCollectionView; }
            set { _notificationsCollectionView = value; }
        }


        private ObservableCollection<Notification> _notifications;

        public ObservableCollection<Notification> Notifications
        {
            get { return _notifications; }
            set
            {
                _notifications = value;
            }
        }


        private ObservableCollection<PrivateLoans> _loanedBooks;

        public ObservableCollection<PrivateLoans> LoanedBooks
        {
            get { return _loanedBooks; }
            set
            {
                _loanedBooks = value;
                OnPropertyChanged("LoanedBooks");
            }
        }

        private bool _yourReservationsEnabled;

        public bool YourReservationsEnabled
        {
            get { return _yourReservationsEnabled; }
            set { _yourReservationsEnabled = value;
                OnPropertyChanged("YourReservationsEnabled");
            }
        }

        private string _passwordBox;

        public string Password
        {
            get { return _passwordBox; }
            set { _passwordBox = value;
                OnPropertyChanged(nameof(Password));
                
                if (Password.Length == 4)
                {
                    CheckPassword(value);
                }
            }
        }

        #endregion
    }
}
