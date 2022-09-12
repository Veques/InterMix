using Intermix.Stores;
using Intermix.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace Intermix.ViewModels
{
    public class LibraryMainWindowViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;



        public LibraryMainWindowViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnViewModelChanged;
            UpdateTime();
        }




        private void OnViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        private async void UpdateTime()
        {
            Time = DateTime.Now.ToString("HH:mm");
            Date = DateTime.Now.Date.ToString("D");
            await Task.Delay(1000);
            UpdateTime();
        }


        #region Properties
        private string _date;

        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        private string _time;
        private NavigationStore navigationStore;

        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged("Time");
            }
        }
        #endregion

    }
}
