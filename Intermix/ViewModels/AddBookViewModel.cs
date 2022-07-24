using Intermix.Commands;
using Intermix.Models;
using Intermix.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Intermix.ViewModels
{
    public class AddBookViewModel : BaseViewModel
    {

        public ICommand AddBookCommand { get; set; }
        public AddBookViewModel()
        {

            var defaultDate = new DateTime(1000, 01, 01);
            AddBookCommand = new RelayCommand(
                o => AddBooks(),
                o => !String.IsNullOrEmpty(Title) && !String.IsNullOrEmpty(AuthorName) && !String.IsNullOrEmpty(AuthorSurname) && PublishDate > defaultDate);
        }

        private void AddBooks()
        {
            using var db = new ApplicationDbContext();


            if (String.IsNullOrEmpty(Publisher))
            {
                Publisher = "";
            }

            db.Books.Add(new Books 
            {
                Title = Title,
                AuthorName = AuthorName,
                AuthorSurname = AuthorSurname,
                PublishDate = PublishDate,
                Publisher = Publisher
            });

            if (db.SaveChanges() >= 1)
            {
                MessageBox.Show("Dodano rekord");
            }

        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value;
                OnPropertyChanged("Title");
            }
        }
        private string _authorName;

        public string AuthorName
        {
            get { return _authorName; }
            set
            {
                _authorName = value;
                OnPropertyChanged("AuthorName");
            }
        }
        private string _authorSurname;

        public string AuthorSurname
        {
            get { return _authorSurname; }
            set
            {
                _authorSurname = value;
                OnPropertyChanged("AuthorSurname");
            }
        }
        private DateTime _publishDate;

        public DateTime PublishDate
        {
            get { return _publishDate; }
            set
            {
                _publishDate = value;
                OnPropertyChanged("PublishDate");
            }
        }

        private string _publisher;

        public string Publisher
        {
            get { return _publisher; }
            set
            {
                _publisher = value;
                OnPropertyChanged("Publisher");
            }
        }


    }
}
