using Zenject;

namespace TicTacToe.Infrastructure.States
{
    public class StateFactory
    {
        private readonly IInstantiator _instantiator;

        public StateFactory(IInstantiator instantiator) => 
            _instantiator = instantiator;

        public TState Create<TState>() where TState : IState => 
            _instantiator.Instantiate<TState>();
    }
}