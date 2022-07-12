using Intermix.Commands;
using Intermix.Models;
using Intermix.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Intermix.ViewModels.LibrarySystem.ForPages
{

    public class ReturnBookModel
    {
        public bool IsChecked { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }

    }

    public class ReturnBooksPageViewModel : BaseViewModel
    {
        public ICommand ReturnBooksCommand { get; set; }
        
        public ReturnBooksPageViewModel()
        {
            ReturnBooksCommand = new RelayCommand(
                o => ReturnBooksClicked(),
                o => true);

            ReturnBooks = new();
            using var db = new ApplicationDbContext();
           

            foreach(var book in db.Books)
            {
                if (book.IsAvailable == 0)
                {
                    ReturnBooks.Add( new ReturnBookModel { 
                        IsChecked = true,
                        Id = book.Id,
                        FullName = $"{book.AuthorName} {book.AuthorSurname}", 
                        Title= book.Title
                    });
                }
            }

        }

        private void ReturnBooksClicked()
        {
            var result = MessageBox.Show("Czy napewno chcesz oddać te książki?", "fdf", MessageBoxButton.YesNo);

            if (result != MessageBoxResult.Yes)
                return;

            using ApplicationDbContext db = new();
            foreach (var book in ReturnBooks)
            {
                if (book.IsChecked)
                {
                    var bookId = book.Id;
                    db.Loans.Remove(db.Loans.SingleOrDefault(s => s.BookId == book.Id));


                    var cell = db.Books.FirstOrDefault(x => x.Id == book.Id);

                    if (cell == null)
                        return;

                    cell.IsAvailable = 1;
                }
            }

            if (db.SaveChanges() > 1)
                MessageBox.Show("Wypożyczenie udane");

            //TODO: możnaby to zoptymalizować
            ReturnBooks.Clear();

            foreach (var book in db.Books)
            {
                if (book.IsAvailable == 0)
                {

                    ReturnBooks.Add(new ReturnBookModel
                    {
                        IsChecked = false,
                        Id = book.Id,
                        FullName = $"{book.AuthorName} {book.AuthorSurname}",
                        Title = book.Title
                    });
                }
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

    }
}
