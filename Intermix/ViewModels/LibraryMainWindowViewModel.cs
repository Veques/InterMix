using Intermix.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Intermix.ViewModels
{

    public class LibraryMainWindowViewModel : BaseViewModel
    {
        private static LibraryMainWindowViewModel _instance = new LibraryMainWindowViewModel();
        public static LibraryMainWindowViewModel Instance { get { return _instance; } }



        private static Visibility _loadingVisibility;
        public Visibility LoadingVisibility
        {
            get { return _loadingVisibility; }
            set {
                _loadingVisibility = value;
                OnPropertyChanged("LoadingVisibility");
            }
        }

    }
}
