using UnityMvvmToolkit.Core.Interfaces;
using Zenject;

namespace TicTacToe.UI.Factories
{
    public class ViewModelFactory
    {
        private readonly IInstantiator _instantiator;

        public ViewModelFactory(IInstantiator instantiator) => 
            _instantiator = instantiator;

        public TViewModel Create<TViewModel>() where TViewModel : IBindingContext => 
            _instantiator.Instantiate<TViewModel>();
    }
}