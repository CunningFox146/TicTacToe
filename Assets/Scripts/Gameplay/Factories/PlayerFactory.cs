using TicTacToe.Services.GameBoard.BoardPlayers;
using Zenject;

namespace TicTacToe.Gameplay.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IInstantiator _instantiator;

        public PlayerFactory(IInstantiator instantiator) => 
            _instantiator = instantiator;

        public TController Create<TController>() where TController : IPlayer => 
            _instantiator.Instantiate<TController>();
    }
}