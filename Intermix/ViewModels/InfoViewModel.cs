using Intermix.Commands;
using Intermix.Stores;
using Intermix.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace Intermix.ViewModels
{
    public class InfoViewModel : BaseViewModel
    {
        public ICommand BackMainCommand { get; set; }

        #region Constructor
        public InfoViewModel(NavigationStore navigationStore)
        {

            BackMainCommand = new NavigationCommand<ChooseActivityViewModel>(navigationStore,
                    () => new ChooseActivityViewModel(navigationStore),
                    x => true);

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
        #endregion

        #region Properties
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
