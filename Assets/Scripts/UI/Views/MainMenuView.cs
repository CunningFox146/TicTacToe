using Cysharp.Threading.Tasks;
using TicTacToe.UI.ViewModels;
using Zenject;

namespace TicTacToe.UI.Views
{
    public class MainMenuView : ViewBase<MainMenuViewModel>
    {
        public class Factory : PlaceholderFactory<string, string, UniTask<MainMenuView>>
        {
        }
    }
}