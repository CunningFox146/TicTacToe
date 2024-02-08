using TicTacToe.UI.Views;
using TicTacToe.UI.ViewStack;

namespace TicTacToe.UI.Factories
{
    public interface IUserInterfaceFactory
    {
        TView CreateView<TView>() where TView : IView;
        ViewStackSystem CreateViewStack();
    }
}