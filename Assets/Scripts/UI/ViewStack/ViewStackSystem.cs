using System;
using System.Collections.Generic;
using TicTacToe.UI.Factories;
using TicTacToe.UI.Views;
using UnityEngine;

namespace TicTacToe.UI.ViewStack
{
    public class ViewStackSystem : MonoBehaviour, IViewStackSystem
    {
        private readonly IUserInterfaceFactory _userInterfaceFactory;
        private readonly Stack<IView> _viewStack = new();

        public IView ActiveView => _viewStack.TryPeek(out var view) ? view : null;

        public ViewStackSystem(IUserInterfaceFactory userInterfaceFactory)
        {
            _userInterfaceFactory = userInterfaceFactory;
        }
        
        public void PushView<TView>() where TView : IView
        {
            var view = _userInterfaceFactory.CreateView<TView>();
            view.Transform.SetParent(transform);
            _viewStack.Push(view);
        }

        public void PopView()
        {
            if (!_viewStack.TryPop(out var view))
                return;

            view.Destroy();
        }
    }
}