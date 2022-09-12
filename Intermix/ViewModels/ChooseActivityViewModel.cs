using Intermix.Commands;
using Intermix.Stores;
using Intermix.ViewModels.Base;
using Intermix.ViewModels.CurrencyConverter;
using Intermix.ViewModels.TicTacToe;
using Intermix.Views;
using System.Windows;
using System.Windows.Input;

namespace Intermix.ViewModels
{
    public class ChooseActivityViewModel : BaseViewModel
    {
        #region Fields
        public ICommand NavigateInfo { get; set; }
        public ICommand NavigateTicTacToe { get; set; }
        public ICommand NavigateLibrary { get; set; }
        public ICommand NavigateConverter { get; set; }
        public ICommand NavigateSnake { get; set; }
        public ICommand CheckItemCommand { get; set; }
        private NavigationStore _navigationStore { get; set; }
        #endregion

        #region Constructor

        public ChooseActivityViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            CheckItemCommand = new RelayCommand(Check, x => true);

            NavigateInfo = new NavigationCommand<InfoViewModel>(_navigationStore,
                    () => new InfoViewModel(_navigationStore),
                    x => true);

            NavigateTicTacToe = new NavigationCommand<GameTypeViewModel>(_navigationStore,
                    () => new GameTypeViewModel(_navigationStore),
                    x => true);

            NavigateConverter = new NavigationCommand<CurrencyConverterViewModel>(_navigationStore,
                    () => new CurrencyConverterViewModel(_navigationStore),
                    x => true);


            //TODO: Theming
            if (Config.Default.Theme == 0)
            {
                DarkLogo = Visibility.Visible;
                LightLogo = Visibility.Collapsed;
            }
            else
            {
                DarkLogo = Visibility.Collapsed;
                LightLogo = Visibility.Visible;
            }

        }

        private void Check(object parameter)
        {
            switch (parameter)
            {
                case "Info":
                    NavigateInfo.Execute(this);
                    break;

                case "TicTacToe":
                    NavigateTicTacToe.Execute(this);
                    break;

                case "CurrencyConverter":
                    NavigateConverter.Execute(this);
                    break;

                case "Library":
                    ShowWindow();
                    break;
            }
        }

        public static void ShowWindow()
        {
            LoginWindow window = new();
            window.ShowDialog();
        }

        #endregion

        #region Properties

        private string _chosenUsername;
        public string ChosenUsername
        {
            get { return _chosenUsername; }
            set
            {
                _chosenUsername = value;
                OnPropertyChanged("ChosenUsername");
            }
        }

        private Visibility _lightLogo;

        public Visibility LightLogo
        {
            get { return _lightLogo; }
            set
            {
                _lightLogo = value;
                OnPropertyChanged("LightLogo");
            }
        }

        private Visibility _darkLogo;

        public Visibility DarkLogo
        {
            get { return _darkLogo; }
            set
            {
                _darkLogo = value;
                OnPropertyChanged("DarkLogo");
            }
        }

        #endregion
    }
}
