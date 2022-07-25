﻿using Intermix.Models;
using Intermix.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Intermix.ViewModels.LibrarySystem.ForUCs
{
    public class BooksDataGridViewModel : BaseViewModel
    {

        public BooksDataGridViewModel()
        {
            using var db = new ApplicationDbContext();
            AllBooks = new(db.Books);

            BooksCollectionView = CollectionViewSource.GetDefaultView(AllBooks);
            BooksCollectionView.Filter = FilterBooks;

        }

        private bool FilterBooks(object obj)
        {
            if (obj is Books book)
            {
                string Fullname = $"{book.AuthorName} {book.AuthorSurname}";

                return book.Title.Contains(TitleFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    book.Publisher.Contains(PublisherFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    book.PublishDate.ToString().Contains(PublishDateFilter, StringComparison.InvariantCultureIgnoreCase) &&
                    Fullname.Contains(AuthorFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        private ICollectionView _booksCollectionView;
        public ICollectionView BooksCollectionView
        {
            get { return _booksCollectionView; }
            set
            {
                _booksCollectionView = value;
                OnPropertyChanged("BooksCollectionView");
            }
        }

        private string _titleFilter = string.Empty;
        public string TitleFilter
        {
            get { return _titleFilter; }
            set
            {
                if (value != null)
                {
                    _titleFilter = value;
                }
                else
                {
                    _titleFilter = String.Empty;
                }

                OnPropertyChanged("TitleFilter");
                BooksCollectionView.Refresh();
            }
        }


        private string _authorFilter = string.Empty;
        public string AuthorFilter
        {
            get { return _authorFilter; }
            set
            {
                if (value != null)
                {
                    _authorFilter = value;
                }
                else
                {
                    _authorFilter = String.Empty;
                }

                OnPropertyChanged("AuthorFilter");
                BooksCollectionView.Refresh();
            }
        }


        private string _publishDateFilter = string.Empty;
        public string PublishDateFilter
        {
            get { return _publishDateFilter; }
            set
            {
                if (value != null)
                {
                    _publishDateFilter = value;
                }
                else
                {
                    _publishDateFilter = String.Empty;
                }

                OnPropertyChanged("PublishDateFilter");
                BooksCollectionView.Refresh();
            }
        }

        private string _publisherFilter = string.Empty;
        public string PublisherFilter
        {
            get { return _publisherFilter; }
            set
            {
                if (value != null)
                {
                    _publisherFilter = value;
                }
                else
                {
                    _publisherFilter = String.Empty;
                }

                OnPropertyChanged("PublisherFilter");
                BooksCollectionView.Refresh();
            }
        }

        private ObservableCollection<Books> _allBooks;
        public ObservableCollection<Books> AllBooks
        {
            get { return _allBooks; }
            set
            {
                _allBooks = value;
                OnPropertyChanged("AllBooks");
            }
        }

    }
}
