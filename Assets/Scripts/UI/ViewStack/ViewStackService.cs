using System.Collections.Generic;
using TicTacToe.UI.Views;

namespace TicTacToe.UI.ViewStack
{
    public class ViewStackService : IViewStackService
    {
        private readonly Stack<IView> _viewStack = new();

        public IView ActiveView => _viewStack.TryPeek(out var view) ? view : null;

        public void PushView(IView view)
        {
            _viewStack.Push(view);
            if (view is ISortable sortable)
                sortable.SetSortOrder(_viewStack.Count);
        }

        public void PopView()
        {
            if (_viewStack.Count > 0)
                _viewStack.Pop().Kill();
        }

        public void ClearStack()
        {
            while (_viewStack.Count > 0)
                PopView();

            _viewStack.Clear();
        }
    }
}