using Intermix.ViewModels.Base;

namespace Intermix.ViewModels.MainWindowView.TicTacToeView
{
    public class GameTypeViewModel : BaseViewModel
    {

        public GameTypeViewModel()
        {

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
