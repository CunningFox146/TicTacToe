using Cysharp.Threading.Tasks;
using TicTacToe.UI.ViewModels;
using Zenject;

namespace TicTacToe.UI.Views
{
    public class GameEndView : ViewBase<GameEndViewModel>
    {
        public class Factory : PlaceholderFactory<string, string, UniTask<GameEndView>>
        {
        }
    }
}