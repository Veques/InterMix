using Intermix.Commands;
using Intermix.Models;
using Intermix.ViewModels.Base;
using Intermix.ViewModels.LoginPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Intermix.ViewModels.LibrarySystem
{   
    public class MainLibraryPageViewModel : BaseViewModel
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

        public MainLibraryPageViewModel()
        {
            LoggedUser = LoginPageViewModel.User;
            UserId = LoginPageViewModel.UserId;



            LoanedBooks = new();
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
            CheckDateAndAddNotifications();

        }
        private void CheckDateAndAddNotifications()
        {
            DateTime date = DateTime.Now.Date;
            Notifications = new();

            foreach (var loanedBook in LoanedBooks)
            {
                var timeSpan = loanedBook.ReturnDate.Subtract(date);

                if (timeSpan.TotalDays <= 3)
                {

                    Notifications.Add(new Notification
                    {
                        Title = loanedBook.Title,
                        ReturnDate = loanedBook.ReturnDate,
                        ExpiringNotifForeground = Brushes.Red

                    });

                    NewNotificationVisibility = Visibility.Visible;
                    NotificationsCount += 1;
                }
            }
        }


        public int UserId { get; private set; }

        private Visibility _mainTabsVisibiliy = Visibility.Visible;
        public Visibility MainTabsVisibility
        {
            get { return _mainTabsVisibiliy; }
            set
            {
                _mainTabsVisibiliy = value;
                OnPropertyChanged("MainTabsVisibility");
            }
        }

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

        private string _loggedUser;

        public string LoggedUser
        {
            get { return _loggedUser; }
            set
            {
                _loggedUser = value;
                OnPropertyChanged("LoggedUser");
            }
        }


    }
}
