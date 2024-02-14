using Cysharp.Threading.Tasks;
using TicTacToe.UI.ViewModels;
using Zenject;

namespace TicTacToe.UI.Views
{
    public class GameModeSelectView : ViewBase<GameModeSelectViewModel>
    {
        public class Factory : PlaceholderFactory<string, string, UniTask<GameModeSelectView>>
        {
        }
    }
}