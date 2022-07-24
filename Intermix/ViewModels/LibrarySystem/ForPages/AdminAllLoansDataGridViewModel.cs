using Intermix.Models;
using Intermix.ViewModels.Base;
using Intermix.ViewModels.LoginPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Intermix.ViewModels.LibrarySystem.ForPages
{
    public class Loan
    {
        public string Username { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
    }

    public class AdminAllLoansDataGridViewModel : BaseViewModel
    {
        public AdminAllLoansDataGridViewModel()
        {
            AllLoans = new();

            using var db = new ApplicationDbContext();

            foreach (var loan in db.Loans)
            {
                foreach (var book in db.Books.Where(x => x.Id == loan.BookId))
                {
                    foreach (var user in db.Users.Where(x => x.Id == loan.UserId))
                    {

                        AllLoans.Add(new Loan
                        {
                            Username = $"{user.Name} {user.Surname}",
                            Title = book.Title,
                            Author = $"{book.AuthorName} {book.AuthorSurname}",
                            LoanDate = loan.LoanDate,
                            ExpectedReturnDate = loan.ExpectedReturn
                        });
                    }
                }
            }
            _data = CollectionViewSource.GetDefaultView(AllLoans);
            _data.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Loan.Username)));
        }


        private ICollectionView _data;

        public ICollectionView Data
        {
            get { return _data; }
            set { _data = value;
                OnPropertyChanged("Data");
            }
        }

        private ObservableCollection<Loan> _allLoans;

        public ObservableCollection<Loan> AllLoans
        {
            get { return _allLoans; }
            set { _allLoans = value;
                OnPropertyChanged("AllLoans");
            }
        }

    }
}
