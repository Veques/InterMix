using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermix.ViewModels.MainWindowView.TicTacToeView
{
    public class ChooseActivityViewModel
    {
        private string _chosenUsername;

        public string ChosenUsername 
        {
            get { return _chosenUsername; }
            set { _chosenUsername = value; }
        }


    }
}
