using Intermix.Commands;
using Intermix.Stores;
using Intermix.ViewModels.Base;
using System.Windows.Input;

namespace Intermix.ViewModels.TicTacToe
{
    public class GameTwoPlayersViewModel : BaseViewModel
    {
        public ICommand BackMainCommand { get; set; }
        public GameTwoPlayersViewModel(NavigationStore navigationStore)
        {
            BackMainCommand = new NavigationCommand<GameTypeViewModel>(navigationStore,
                () => new GameTypeViewModel(navigationStore),
                x => true);
        }
    }
}
