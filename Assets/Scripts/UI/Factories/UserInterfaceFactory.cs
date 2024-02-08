using System;
using System.Collections.Generic;
using TicTacToe.UI.Views;
using TicTacToe.UI.ViewStack;
using Zenject;
using Object = UnityEngine.Object;

namespace TicTacToe.UI.Factories
{
    public class UserInterfaceFactory : IUserInterfaceFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly UserInterfaceAssets _assets;
        private readonly Dictionary<Type, Object> _viewPrefabs;

        public UserInterfaceFactory(IInstantiator instantiator, UserInterfaceAssets assets)
        {
            _instantiator = instantiator;
            _assets = assets;
            _viewPrefabs = new Dictionary<Type, Object>
            {
                [typeof(MainMenuView)] = assets.MainMenuViewPrefab,
            };
        }

        public ViewStackSystem CreateViewStack()
            => _instantiator.InstantiatePrefab(_assets.ViewStackPrefab).GetComponent<ViewStackSystem>();
        
        public TView CreateView<TView>() where TView : IView
            => _instantiator.InstantiatePrefab(_viewPrefabs[typeof(TView)]).GetComponent<TView>();
    }
}