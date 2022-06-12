using Intermix.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermix.Helpers
{
    public class UsedTheme : BaseViewModel
    {
        private bool _darkTheme;

        public bool DarkTheme
        {
            get { return _darkTheme; }
            set { _darkTheme = value;
                OnPropertyChanged("DarkTheme");
            }
        }

        private bool _lightTheme;

        public bool LightTheme
        {
            get { return _lightTheme; }
            set
            {
                _lightTheme = value;
                OnPropertyChanged("LightTheme");
            }
        }

    }
}
