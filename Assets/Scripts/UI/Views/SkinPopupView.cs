using Cysharp.Threading.Tasks;
using TicTacToe.UI.ViewModels;
using Zenject;

namespace TicTacToe.UI.Views
{
    public class SkinPopupView : ViewBase<SkinPopupViewModel>
    {
        public class Factory : PlaceholderFactory<string, string, UniTask<SkinPopupView>>
        {
        }
    }
}