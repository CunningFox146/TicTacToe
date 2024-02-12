using Cysharp.Threading.Tasks;
using TicTacToe.UI.ViewModels;
using Zenject;

namespace TicTacToe.UI.Views
{
    public class HUDView : ViewBase<HUDViewModel>
    {
        public class Factory : PlaceholderFactory<string, string, UniTask<HUDView>>
        {
        }
    }
}