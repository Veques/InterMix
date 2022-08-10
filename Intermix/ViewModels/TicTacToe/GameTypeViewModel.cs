using Intermix.Commands;
using Intermix.Stores;
using Intermix.ViewModels.Base;
using System;
using System.Windows.Input;

namespace Intermix.ViewModels.TicTacToe
{
    public class GameTypeViewModel : BaseViewModel
    {
        public ICommand StartCommand { get; set; }
        public ICommand OnePlayerEZCommand { get; set; }
        public ICommand OnePlayerHDCommand { get; set; }
        public ICommand TwoPlayersCommand { get; set; }
        public ICommand BackMainCommand { get; set; }
        
        public GameTypeViewModel()
        {

        }

        public GameTypeViewModel(NavigationStore navigationStore)
        {

            StartCommand = new RelayCommand(x => CheckGameType(),
                x => OnePlayer == true || (EasyMode == true || EasyMode != true));

            OnePlayerEZCommand = new NavigationCommand<GameOnePlayerEasyViewModel>(navigationStore,
                () => new GameOnePlayerEasyViewModel(navigationStore),
                x => true);
            OnePlayerHDCommand = new NavigationCommand<GameOnePlayerHardViewModel>(navigationStore,
                () => new GameOnePlayerHardViewModel(navigationStore),
                x => true);
            TwoPlayersCommand = new NavigationCommand<GameTwoPlayersViewModel>(navigationStore,
                () => new GameTwoPlayersViewModel(navigationStore),
                x => true);
            BackMainCommand = new NavigationCommand<ChooseActivityViewModel>(navigationStore,
                () => new ChooseActivityViewModel(navigationStore),
                x => true);
        }

        private void CheckGameType()
        {
            switch (OnePlayer)
            {
                case false:
                    TwoPlayersCommand.Execute(this);
                    break;

                case true:
                    {
                        if (EasyMode)
                        {
                            OnePlayerEZCommand.Execute(this);
                        }
                        else
                        {
                            OnePlayerHDCommand.Execute(this);
                        }
                        break;
                    }
            }
        }

        #region Properties

        private bool _onePlayer;
        public bool OnePlayer
        {
            get { return _onePlayer; }
            set
            {
                _onePlayer = value;
                OnPropertyChanged("OnePlayer");
            }
        }

        private bool _easyMode;

        public bool EasyMode
        {
            get { return _easyMode; }
            set
            {
                _easyMode = value;
                OnPropertyChanged("EasyMode");
            }
        }

        private bool _isSelectableDifficulty = true;

        public bool IsSelectableDifficulty
        {
            get { return _isSelectableDifficulty; }
            set
            {
                _isSelectableDifficulty = value;
                OnPropertyChanged("IsSelectableDifficulty");
            }
        }

        private bool _isStartClickable = false;

        public bool IsStartClickable
        {
            get { return _isStartClickable; }
            set
            {
                _isStartClickable = value;
                OnPropertyChanged("IsStartClickable");
            }
        }

        #endregion
    }
}
