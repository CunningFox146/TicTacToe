using TicTacToe.UI.ViewModels;
using UnityMvvmToolkit.UGUI;

namespace TicTacToe.UI.Views
{
    public class MainMenuView : CanvasView<MainMenuViewModel>
    {
        protected override MainMenuViewModel GetBindingContext()
        {
            return new MainMenuViewModel();
        }
    }
}