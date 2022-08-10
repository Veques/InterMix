using Intermix.Stores;
using Intermix.ViewModels.Base;
using Intermix.Commands;
using System.Windows;
using System.Windows.Input;
using System;
using Intermix.ViewModels.TicTacToe;
using Intermix.ViewModels.CurrencyConverter;
using Intermix.Views;

namespace Intermix.ViewModels
{
    public class ChooseActivityViewModel : BaseViewModel
    {
        #region Fields
        public ICommand NavigateInfo { get; set; }
        public ICommand NavigateTicTacToe { get; set; }
        public ICommand NavigateLibrary { get; set; }
        public ICommand NavigateConverter { get; set; }
        public ICommand CheckItemCommand { get; set; }
        private NavigationStore _navigationStore { get; set; }
        #endregion

        #region Constructors

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
                DarkLogo_TTT = Visibility.Visible;
                LightLogo_TTT = Visibility.Collapsed;
            }
            else
            {
                DarkLogo_TTT = Visibility.Collapsed;
                LightLogo_TTT = Visibility.Visible;
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

                    //TODO: LIbrary
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

        private Visibility _lightLogo_TTT;

        public Visibility LightLogo_TTT
        {
            get { return _lightLogo_TTT; }
            set
            {
                _lightLogo_TTT = value;
                OnPropertyChanged("LightLogo_TTT");
            }
        }

        private Visibility _darkLogo_TTT;

        public Visibility DarkLogo_TTT
        {
            get { return _darkLogo_TTT; }
            set
            {
                _darkLogo_TTT = value;
                OnPropertyChanged("DarkLogo_TTT");
            }
        }

        #endregion
    }
}
