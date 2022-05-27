using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Intermix.ViewModels.MainWindowView.TicTacToeView
{
    public class GamePageViewModel
    {
        public void NewGame(bool onePlayer, bool easyMode)
        {
            
            MessageBox.Show(onePlayer.ToString());
            MessageBox.Show(easyMode.ToString());
        }
    }
}
