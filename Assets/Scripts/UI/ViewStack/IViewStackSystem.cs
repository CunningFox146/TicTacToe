using TicTacToe.UI.Views;

namespace TicTacToe.UI.ViewStack
{
    public interface IViewStackSystem
    {
        IView ActiveView { get; }
        void PushView<TView>() where TView : IView;
        void PopView();
    }
}