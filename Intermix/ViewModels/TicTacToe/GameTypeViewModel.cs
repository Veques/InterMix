using Intermix.Commands;
using Intermix.Stores;
using Intermix.ViewModels.Base;
using System.Windows.Input;

namespace Intermix.ViewModels.TicTacToe
{
    public class GameTypeViewModel : BaseViewModel
    {
        public ICommand StartCommand { get; set; }
        public ICommand OnePlayerEZCommand { get; set; }
        public ICommand TwoPlayersCommand { get; set; }
        public ICommand BackMainCommand { get; set; }

        public GameTypeViewModel(NavigationStore navigationStore)
        {

            StartCommand = new RelayCommand(x => CheckGameType(),
                x => ValidateChoice());

            OnePlayerEZCommand = new NavigationCommand<GameOnePlayerEasyViewModel>(navigationStore,
                () => new GameOnePlayerEasyViewModel(navigationStore),
                x => true);
            TwoPlayersCommand = new NavigationCommand<GameTwoPlayersViewModel>(navigationStore,
                () => new GameTwoPlayersViewModel(navigationStore),
                x => true);
            BackMainCommand = new NavigationCommand<ChooseActivityViewModel>(navigationStore,
                () => new ChooseActivityViewModel(navigationStore),
                x => true);
        }

        private bool ValidateChoice()
        {
            if (HardMode == false && EasyMode == false && TwoPlayers == false)
            {
                return false;
            }
            return true;
        }

        private void CheckGameType()
        {
            if (EasyMode == true)
            {
                OnePlayerEZCommand.Execute(this);
            }
            if (TwoPlayers == true)
            {
                TwoPlayersCommand.Execute(this);
            }
        }

        #region Properties

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
        private bool _hardMode;

        public bool HardMode
        {
            get { return _hardMode; }
            set
            {
                _hardMode = value;
                OnPropertyChanged("HardMode");
            }
        }
        private bool _twoPlayers;

        public bool TwoPlayers
        {
            get { return _twoPlayers; }
            set
            {
                _twoPlayers = value;
                OnPropertyChanged("TwoPlayers");
            }
        }


        #endregion
    }
}
