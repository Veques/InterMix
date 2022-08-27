﻿using Intermix.Stores;
using Intermix.ViewModels.Base;
using System.Windows.Input;

namespace Intermix.ViewModels
{
    public class LoginWindowViewModel : BaseViewModel
    {
        readonly NavigationStore _navigationStore;
        public ICommand ExitCommand { get; set; }

        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;

        public LoginWindowViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnViewModelChanged;

            //ExitCommand = new RelayCommand(x => foreach(var window in Application.Current.Windows) { window.Close(); },

        }

        private void OnViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }

}
