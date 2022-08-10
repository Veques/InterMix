using Intermix.Commands;
using Intermix.Models;
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
using Reservations = Intermix.Models.LibrarySystemModels.Loan;

namespace Intermix.ViewModels.LibrarySystem.ForPages
{
    public class AdminAllReservationsDataGridViewModel : BaseViewModel
    {
        public ICommand ClearFilterCommand { get; set; }
        public ICommand BackMainCommand { get; set; }

        #region Constructor
        public AdminAllReservationsDataGridViewModel(Stores.NavigationStore navigationStore)
        {
            AllReservations = new();
            Users = new();
            ClearFilterCommand = new RelayCommand(o => ClearFilter() , o => !SelectedUser.Equals(String.Empty));

            BackMainCommand = new NavigationCommand<AdminPageViewModel>(navigationStore, 
                () => new AdminPageViewModel(navigationStore),
                x => true);

            FillDataGrid();

            ReservationCollectionView = CollectionViewSource.GetDefaultView(AllReservations);
            ReservationCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Reservations.Username)));
            ReservationCollectionView.Filter = Filter;
        }
        #endregion
        private void ClearFilter()
        {
            SelectedIndex = -1;
            SelectedUser = String.Empty;
        }

        #region Fill Data Grid
        private void FillDataGrid()
        {
            using var db = new ApplicationDbContext();

            foreach (var reservation in db.Reservations)
            {
                foreach (var book in db.Books.Where(x => x.Id == reservation.BookId))
                {
                    foreach (var user in db.Users.Where(x => x.Id == reservation.UserId))
                    {

                        if (!Users.Contains($"{user.Name} {user.Surname}"))
                        {
                            Users.Add($"{user.Name} {user.Surname}");
                        }

                        AllReservations.Add(new Reservations
                        {
                            Username = $"{user.Name} {user.Surname}",
                            Title = book.Title,
                            Author = $"{book.AuthorName} {book.AuthorSurname}",
                            ExpectedReturnDate = reservation.ExpectedReturn,
                            LastReservationDay = reservation.ExpectedReturn.Date.AddDays(2)
                            
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

            if (obj is Reservations reservation)
            {
                return reservation.Title.Contains(TitleFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    reservation.Author.Contains(AuthorFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    reservation.ExpectedReturnDate.ToString().Contains(ReturnDateFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    reservation.LastReservationDay.ToString().Contains(ReservationEndFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    reservation.Username.Contains(SelectedUser, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
        #endregion

        #region Properties

        private ICollectionView _reservationsCollectionView;
        public ICollectionView ReservationCollectionView
        {
            get { return _reservationsCollectionView; }
            set
            {
                _reservationsCollectionView = value;
                OnPropertyChanged("ReservationCollectionView");
            }
        }


        private ObservableCollection<Reservations> _allReservations;
        public ObservableCollection<Reservations> AllReservations
        {
            get { return _allReservations; }
            set
            {
                _allReservations = value;
                OnPropertyChanged("AllReservations");
            }
        }

        private ObservableCollection<string> _users;

        public ObservableCollection<string> Users
        {
            get { return _users; }
            set
            {
                _users = value;
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
                ReservationCollectionView.Refresh();
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
            set
            {
                _titleFilter = value != null ? _titleFilter = value : _titleFilter = string.Empty;

                OnPropertyChanged("TitleFilter");
                ReservationCollectionView.Refresh();
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
                ReservationCollectionView.Refresh();
            }
        }

        private string _reservationEndFilter = string.Empty;
        public string ReservationEndFilter
        {
            get { return _reservationEndFilter; }
            set
            {
                _reservationEndFilter = value != null ? _reservationEndFilter = value : _reservationEndFilter = string.Empty;

                OnPropertyChanged("ReservationEndFilter");
                ReservationCollectionView.Refresh();
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
                ReservationCollectionView.Refresh();
            }
        }

        #endregion

    }
}
