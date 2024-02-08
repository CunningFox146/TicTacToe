using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TicTacToe.UI.Views;
using Zenject;

namespace TicTacToe.UI.ViewStack
{
    public class ViewStackSystem : IViewStackSystem, IDisposable
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
        
        public void Dispose()
        {
            ClearStack();
        }
    }
}