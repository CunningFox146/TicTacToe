using System.Collections.Generic;
using TicTacToe.UI.Views;

namespace TicTacToe.UI.ViewStack
{
    public class ViewStackSystem : IViewStackSystem
    {
        private readonly Stack<IView> _viewStack = new();

        public IView ActiveView => _viewStack.TryPeek(out var view) ? view : null;

        public void PushView(IView view)
            => _viewStack.Push(view);

        public void PopView()
            => ActiveView?.Destroy();

        public void ClearStack()
        {
            while (_viewStack.Count > 0)
                PopView();
            
            _viewStack.Clear();
        }
    }
}