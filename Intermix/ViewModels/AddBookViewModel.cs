﻿using Intermix.Commands;
using Intermix.Enums;
using Intermix.Models;
using Intermix.ViewModels.Base;
using Intermix.Views;
using System;
using System.Windows.Input;

namespace Intermix.ViewModels
{
    public class AddBookViewModel : BaseViewModel
    {
        public ICommand AddBookCommand { get; set; }



        public AddBookViewModel()
        {
            PublishDate = DateTime.Now.Date;

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
                Publisher = Publisher,
                IsAvailable = 1,
                IsReserved = 0

            });

            if (db.SaveChanges() > 0)
            {
                _ = new CustomizedMessageBox("Book has been added", MessageType.Information, MessageButton.Ok).ShowDialog();
                Title = String.Empty;
                AuthorName = String.Empty;
                AuthorSurname = String.Empty;
                Publisher = String.Empty;
            }

        }

        #region Properties
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
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

        #endregion
    }
}
