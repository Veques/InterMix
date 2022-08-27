using Intermix.Commands;
using Intermix.Stores;
using Intermix.ViewModels.Base;
using Intermix.Views;
using System.Windows.Input;

namespace Intermix.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;

        public ICommand OpenRatingCommand { get; set; }
        public ICommand OpenSettingsCommand { get; set; }

        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainWindowViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnViewModelChange;

            OpenRatingCommand = new RelayCommand(x => ShowRatingWindow(), x => true);
            OpenSettingsCommand = new RelayCommand(x => ShowSettingsWindow(), x => true);
        }

        private static void ShowSettingsWindow()
        {
            Settings window = new();
            window.ShowDialog();
        }

        private static void ShowRatingWindow()
        {
            RateWindow window = new();
            window.ShowDialog();
        }

        private void OnViewModelChange()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
