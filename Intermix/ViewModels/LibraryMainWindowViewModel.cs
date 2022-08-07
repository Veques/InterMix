using Intermix.Commands;
using Intermix.Models;
using Intermix.ViewModels.Base;
using Intermix.ViewModels.LoginPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Intermix.ViewModels
{


    public class LibraryMainWindowViewModel : BaseViewModel
    {


        public LibraryMainWindowViewModel()
        {

            UpdateTime();
        }

        private async void UpdateTime()
        {
            Time = DateTime.Now.ToString("HH:mm");
            Date = DateTime.Now.Date.ToString("D");
            await Task.Delay(1000);
            UpdateTime();
        }

        private string _date;

        public string Date
        {
            get { return _date; }
            set { _date = value;
                OnPropertyChanged("Date");
            }
        }

        private string _time;

        public string Time
        {
            get { return _time; }
            set { _time = value;
                OnPropertyChanged("Time");
            }
        }


    }
}
