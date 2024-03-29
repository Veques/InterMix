﻿using Intermix.Commands;
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

    public class ReturnBooksPageViewModel : BaseViewModel
    {
        #region Fields
        public ICommand ReturnBooksCommand { get; set; }
        public ICommand ExtendCommand { get; set; }
        public ICommand BackMainCommand { get; set; }

        #endregion

        #region Constructor
        public ReturnBooksPageViewModel(NavigationStore navigationStore)
        {
            using var db = new ApplicationDbContext();

            ReturnBooksCommand = new RelayCommand(
                o => ReturnBooksClicked(),
                o => ReturnBooks.Any(x => x.IsChecked == true
                ));

            BackMainCommand = new NavigationCommand<MainLibraryPageViewModel>(navigationStore,
                () => new MainLibraryPageViewModel(navigationStore),
                x => true);

            ExtendCommand = new RelayCommand(ExtendLoan, x => true);

            ReturnBooks = new();
            FillDataGrid();


            ReturnBooksCollectionView = CollectionViewSource.GetDefaultView(ReturnBooks);
            ReturnBooksCollectionView.Filter = Filter;

        }

        #endregion

        #region Methods

        private void ExtendLoan(object parameter)
        {
            using var db = new ApplicationDbContext();

            if (parameter == null)
                return;

            bool? result = new CustomizedMessageBox("Are you sure you want to extend your Loan?", MessageType.Confirmation, MessageButton.YesNo).ShowDialog();

            if (!result.Value)
                return;

            if (parameter is ReturnBook book)
            {
                db.Loans.Single(x => x.BookId == book.Id).ExpectedReturn = book.ReturnDate.AddDays(7);
                db.Loans.Single(x => x.BookId == book.Id).WasExtended = 1;

                if (db.SaveChanges() > 0)
                {
                    _ = new CustomizedMessageBox("Saved", MessageType.Success, MessageButton.Ok).ShowDialog();

                    ReturnBooks.Clear();
                    FillDataGrid();
                }
            }
        }

        private bool Filter(object obj)
        {
            if (obj is ReturnBook book)
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
                    ReturnBooks.Add(new ReturnBook
                    {
                        IsChecked = false,
                        IsEnabled = book.IsReserved == 0 && loan.WasExtended == 0,
                        Id = book.Id,
                        FullName = $"{book.AuthorName} {book.AuthorSurname}",
                        Title = book.Title,
                        LoanDate = loan.LoanDate,
                        ReturnDate = loan.ExpectedReturn,
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

        #endregion

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

        private ObservableCollection<ReturnBook> _returnBooks;

        public ObservableCollection<ReturnBook> ReturnBooks
        {
            get { return _returnBooks; }
            set
            {
                _returnBooks = value;
                OnPropertyChanged("ReturnBooks");
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

        #endregion

    }
}
