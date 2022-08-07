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
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Intermix.ViewModels.LibrarySystem.ForPages
{

    public class ReturnBookModel
    {
        public bool IsChecked { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }

    }
    public class ReturnBooksPageViewModel : BaseViewModel
    {
        public ICommand ReturnBooksCommand { get; set; }
        
        public ReturnBooksPageViewModel()
        {
            ReturnBooksCommand = new RelayCommand(
                o => ReturnBooksClicked(),
                o => ReturnBooks.Any(x => x.IsChecked == true
                ));

            ReturnBooks = new();
            FillDataGrid();


            ReturnBooksCollectionView = CollectionViewSource.GetDefaultView(ReturnBooks);
            ReturnBooksCollectionView.Filter = Filter;

        }

        private bool Filter(object obj)
        {
            if (obj is ReturnBookModel book)
            {
                return book.Title.Contains(TitleFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    book.FullName.Contains(AuthorFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    book.LoanDate.ToString().Contains(LoanDateFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    book.ReturnDate.ToString().Contains(ReturnDateFilter, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        private void FillDataGrid()
        {
            using var db = new ApplicationDbContext();

            foreach (var loan in db.Loans.Where(d => d.UserId == LoginPageViewModel.UserId))
            {
                foreach (var book in db.Books.Where(d => d.Id == loan.BookId))
                {

                    ReturnBooks.Add(new ReturnBookModel
                    {
                        IsChecked = false,
                        Id = book.Id,
                        FullName = $"{book.AuthorName} {book.AuthorSurname}",
                        Title = book.Title,
                        LoanDate = loan.LoanDate,
                        ReturnDate = loan.ExpectedReturn
                    });
                }
            }
        }

        private void ReturnBooksClicked()
        {
            bool? result = new CustomizedMessageBox("Do you really want to return these books?", MessageType.Confirmation, MessageButton.OkCancel).ShowDialog();

            if (!result.Value)
                return;

            using ApplicationDbContext db = new();
            foreach (var book in ReturnBooks)
            {
                if (book.IsChecked)
                {
                    db.Loans.Remove(db.Loans.SingleOrDefault(s => s.BookId == book.Id));

                    if (db.Reservations.Any(x => x.BookId == book.Id))
                    {
                        foreach (var reservation in db.Reservations.Where(x => x.BookId == book.Id))
                        {
                            reservation.ReturnDate = DateTime.Now.Date;
                        }
                    }                    


                    var cell = db.Books.FirstOrDefault(x => x.Id == book.Id);

                    if (cell.IsReserved == 0)
                    {
                        if (cell == null)
                        return;

                        cell.IsAvailable = 1;
                    }

                }
            }

            if (db.SaveChanges() > 0)
            {
                _ = new CustomizedMessageBox("Return Successful", MessageType.Success, MessageButton.Ok).ShowDialog();
            }
            else
            {
               _ = new CustomizedMessageBox("Something went wrong", MessageType.Warning, MessageButton.Ok).ShowDialog();
            }

            ReturnBooks.Clear();
            FillDataGrid();
        }

        #region Properties

        private ICollectionView _returnBooksCollectionView;
        public ICollectionView ReturnBooksCollectionView
        {
            get { return _returnBooksCollectionView; }
            set
            {
                _returnBooksCollectionView = value;
                OnPropertyChanged("ReturnBooksCollectionView");
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
                ReturnBooksCollectionView.Refresh();
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
                ReturnBooksCollectionView.Refresh();
            }
        }


        private string _loanDateFilter = string.Empty;
        public string LoanDateFilter
        {
            get { return _loanDateFilter; }
            set
            {
                _loanDateFilter = value != null ? _loanDateFilter = value : _loanDateFilter = string.Empty;
                OnPropertyChanged("LoanDateFilter");
                ReturnBooksCollectionView.Refresh();
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
                ReturnBooksCollectionView.Refresh();
            }
        }

        private ObservableCollection<ReturnBookModel> _returnBooks;

        public ObservableCollection<ReturnBookModel> ReturnBooks
        {
            get { return _returnBooks; }
            set { _returnBooks = value;
                OnPropertyChanged("ReturnBooks");
            }
        }
        #endregion

    }
}
