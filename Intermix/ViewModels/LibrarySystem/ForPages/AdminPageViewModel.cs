using Intermix.Commands;
using Intermix.Enums;
using Intermix.Models;
using Intermix.Stores;
using Intermix.ViewModels.Base;
using Intermix.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Intermix.ViewModels.LibrarySystem.ForPages
{
    public class AdminPageViewModel : BaseViewModel
    {
        #region Commands
        public ICommand AddBookCommand { get; set; }
        public ICommand ChangePINCommand { get; set; }
        public ICommand ImportBooksCommand { get; set; }
        public ICommand BrowseReservationsCommand { get; set; }
        public ICommand BrowseLoansCommand { get; set; }
        public ICommand BackMainCommand { get; set; }

        #endregion

        #region Constructor
        public AdminPageViewModel(NavigationStore navigationStore)
        {
            using var db = new ApplicationDbContext();

            AddBookCommand = new RelayCommand(
                o => AddBook(),
                o => true);

            ChangePINCommand = new RelayCommand(
                o => ChangePIN(),
                o => Validate(FirstPin, SecondPin)
                );

            ImportBooksCommand = new RelayCommand(
                o => ImportBooks(),
                o => true
                );

            BrowseReservationsCommand = new NavigationCommand<AdminAllReservationsDataGridViewModel>(navigationStore,
                () => new AdminAllReservationsDataGridViewModel(navigationStore),
                x => true);

            BrowseLoansCommand = new NavigationCommand<AdminAllLoansDataGridViewModel>(navigationStore,
                () => new AdminAllLoansDataGridViewModel(navigationStore),
                x => true);

            BackMainCommand = new NavigationCommand<MainLibraryPageViewModel>(navigationStore,
                () => new MainLibraryPageViewModel(navigationStore),
                x => true);


        }
        #endregion

        #region AddBook
        private void AddBook()
        {
            AddBookWindow window = new();
            window.ShowDialog();
        }

        #endregion

        #region ImportBooks
        private void ImportBooks()
        {
            using var db = new ApplicationDbContext();

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Other\Top100Books.xlsx");
            try
            {
                OleDbConnection connection = new((@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';"));
                connection.Open();
                OleDbCommand command = new("SELECT * FROM [" + "Arkusz1" + "$]", connection);
                DataTable Data = new();
                OleDbDataAdapter adapter = new(command);
                adapter.Fill(Data);
                connection.Close();
                for (int i = Data.Rows.Count - 1; i >= 0; i--)
                {
                    if (Data.Rows[i][0].ToString() == String.Empty)
                    {
                        Data.Rows.RemoveAt(i);
                    }
                }

                List<Books> list = new();

                list = (from DataRow dr in Data.Rows
                        select new Books()
                        {
                            Id = Convert.ToInt32(dr["Position"]),
                            Title = dr["Title"].ToString(),
                            AuthorName = dr["Name"].ToString(),
                            AuthorSurname = dr["Surname"].ToString(),
                            PublishDate = (DateTime)dr["DataWydania"],
                            Publisher = dr["Wydawnictwo"].ToString(),
                            IsAvailable = 1,
                            IsReserved = 0
                        }).ToList();

                foreach (Books item in list)
                {
                    db.Books.Add(item);
                }
                if (db.SaveChanges() > 0)
                {
                    _ = new CustomizedMessageBox("Dodano 100 rekordów z pliku Excel", MessageType.Information, MessageButton.Ok).ShowDialog();

                    IsEnabled = false;
                }

            }
            catch (OleDbException ex)
            {
                _ = new CustomizedMessageBox(ex.Message, MessageType.Error, MessageButton.Ok).ShowDialog();

            }


        }


        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        #endregion

        #region ChangePin
        private static bool Validate(string first, string second)
        {
            if (first == null || second == null)
                return false;

            if (first.Equals(second))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ChangePIN()
        {
            using var db = new ApplicationDbContext();

            foreach(var user in db.Users.Where(d => d.Username.Equals("Admin")))
            {
                user.Password = FirstPin;
            }
            if (db.SaveChanges() > 0)
            {
                _ = new CustomizedMessageBox("Saved!", MessageType.Success, MessageButton.Ok).ShowDialog();
            }
            else
            {
                _ = new CustomizedMessageBox("Error has occured - try another PIN", MessageType.Error, MessageButton.Ok).ShowDialog();

            }
        }

        private string _firstPIN;
        public string FirstPin
        {
            get { return _firstPIN; }
            set { _firstPIN = value;
                OnPropertyChanged("FirstPIN");
            }
        }
        private string _secondPIN;
        public string SecondPin
        {
            get { return _secondPIN; }
            set
            {
                _secondPIN = value;
                OnPropertyChanged("SecondPin");
            }
        }
        #endregion
    }
}
