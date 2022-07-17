using Intermix.Commands;
using Intermix.Models;
using Intermix.ViewModels.Base;
using Intermix.ViewModels.LoginPage;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System;

namespace Intermix.ViewModels.LibrarySystem.ForPages
{

    public class LoanBooksModel 
    {
        public bool IsChecked { get; set; }
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? FullName { get; set; }
        public DateTime PublishDate { get; set; }
        public string Publisher { get; set; }
        public int? IsAvailable { get; set; }
    }

    public class LoanBooksPageViewModel : BaseViewModel
    {
        public ICommand LoanCommand { get; set; }

        public LoanBooksPageViewModel()
        {
            LoanCommand = new RelayCommand(
                o => LoanClicked(),
                o => true);

            LoanBooks = new();
            FillDataGrid();

        }

        private void FillDataGrid()
        {            
            using var db = new ApplicationDbContext();
            foreach (var book in db.Books.Where(d => d.IsAvailable == 1))
            {
                LoanBooks.Add(new LoanBooksModel
                {
                    IsChecked = false,
                    Id = book.Id,
                    FullName = $"{book.AuthorName} {book.AuthorSurname}",
                    Title = book.Title,
                    PublishDate = book.PublishDate,
                    Publisher = book.Publisher
                });
            }
        }

        private void LoanClicked()
        {
            var result = MessageBox.Show("Czy napewno chcesz wypożyczyć te książki?", "fdf",MessageBoxButton.YesNo);

            if (result != MessageBoxResult.Yes)
                return;

            using ApplicationDbContext db = new();
            foreach (var book in LoanBooks.Where(a => a.IsChecked == true))
            {
                db.Loans.Add(new Loans { 
                    UserId= LoginPageViewModel.UserId,
                    BookId = book.Id,
                    LoanDate = DateTime.Now.Date,
                    ExpectedReturn = DateTime.Now.Date.AddDays(14)
                });

                var cell = db.Books.FirstOrDefault(x => x.Id == book.Id);

                if (cell == null)
                    return;
                    
                cell.IsAvailable = 0;
            }

            if(db.SaveChanges() > 1) 
                MessageBox.Show("Wypożyczenie udane");

            LoanBooks.Clear();
            FillDataGrid();
        }

        private ObservableCollection<Books> _books;

        public ObservableCollection<Books> Books
        {
            get { return Books; }
            set { Books = value;
                OnPropertyChanged("Books");
            }
        }


        private ObservableCollection<LoanBooksModel> _loanBooks;
        public ObservableCollection<LoanBooksModel> LoanBooks
        {
            get { return _loanBooks; }
            set
            {
                _loanBooks = value;
                OnPropertyChanged("LoanBooks");
            }
        }
    }
}
