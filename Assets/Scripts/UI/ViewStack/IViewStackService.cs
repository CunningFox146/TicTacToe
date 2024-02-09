using TicTacToe.UI.Views;

namespace TicTacToe.UI.ViewStack
{
    public interface IViewStackService
    {
        IView ActiveView { get; }
        void PushView(IView view);
        void PopView();
        void ClearStack();
    }
}