using Intermix.ViewModels.Base;
using System;

namespace Intermix.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;

        private BaseViewModel _baseViewModel;

        public BaseViewModel CurrentViewModel
        {
            get { return _baseViewModel; }
            set
            {
                _baseViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }


    }
}
