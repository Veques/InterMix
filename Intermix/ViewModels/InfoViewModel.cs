using Intermix.ViewModels.Base;
using System.Windows;

namespace Intermix.ViewModels
{
    public class InfoViewModel : BaseViewModel
    {

        public InfoViewModel()
        {
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

    }
}
