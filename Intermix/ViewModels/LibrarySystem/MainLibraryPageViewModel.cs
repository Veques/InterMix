using Intermix.Commands;
using Intermix.Models;
using Intermix.Stores;
using Intermix.ViewModels.Base;
using Intermix.ViewModels.LibrarySystem.ForPages;
using Intermix.ViewModels.LoginPage;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Intermix.ViewModels.LibrarySystem
{
    #region Models
    public class PrivateLoans
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }

    public class Notification
    {
        public string Title { get; set; }
        public DateTime ReturnDate { get; set; }
        public SolidColorBrush ExpiringNotifForeground { get; set; }
    }
    #endregion

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
            CheckDateAndAddNotifications();

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
        }
        #endregion

        private void CheckPassword(string value)
        {
            using var db = new ApplicationDbContext();

            if (value == db.Users.Single(x => x.Username == "Admin").Password)
            {
                AdminCommand.Execute(this);
            }
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
        private void CheckDateAndAddNotifications()
        {
            DateTime date = DateTime.Now.Date;
            Notifications = new();

            foreach (var loanedBook in LoanedBooks)
            {
                var timeSpan = loanedBook.ReturnDate.Subtract(date);

                if (timeSpan.TotalDays <= 5)
                {

                    Notifications.Add(new Notification
                    {
                        Title = loanedBook.Title,
                        ReturnDate = loanedBook.ReturnDate,
                        ExpiringNotifForeground = Brushes.Red

                    });

                    NotificationsCount += 1;
                }
            }
        }

        #endregion

        #region Properties

        public static int UserId { get; private set; }
        public static string LoggedUser { get; private set; }

        private Visibility _newNotificationVisibility = Visibility.Collapsed;

        public Visibility NewNotificationVisibility
        {
            get { return _newNotificationVisibility; }
            set
            {
                _newNotificationVisibility = value;
                OnPropertyChanged("NewNotificationVisibility");
            }
        }

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
                    Password = String.Empty;
                }
            }
        }

        #endregion
    }
}
