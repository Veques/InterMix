using Intermix.Commands;
using Intermix.Models;
using Intermix.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        ApplicationDbContext db = new();
        public ICommand ChangePINCommand { get; set; }

        public AdminPageViewModel()
        {
            ChangePINCommand = new RelayCommand(
                o => ChangePIN(),
                o => Validate(FirstPin, SecondPin)
                );
        }
        private bool Validate(string first, string second)
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

            foreach(var user in db.Users.Where(d => d.Username.Equals("Admin")))
            {
                user.Password = FirstPin;
            }
            if (db.SaveChanges() > 1)
            {
                MessageBox.Show("Zapisano");
            }

        }

        private string _firstPIN;
        private string _secondPIN;
        public string FirstPin
        {
            get { return _firstPIN; }
            set { _firstPIN = value;
                OnPropertyChanged("FirstPIN");
            }
        }
        public string SecondPin
        {
            get { return _secondPIN; }
            set
            {
                _secondPIN = value;
                OnPropertyChanged("SecondPin");
            }
        }

    }
}
