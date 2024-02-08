using TicTacToe.UI.Views;

namespace TicTacToe.UI.ViewStack
{
    public interface IViewStackSystem
    {
        IView ActiveView { get; }
        void PushView(IView view);
        void PopView();
        void ClearStack();
    }
}