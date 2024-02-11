using TicTacToe.Services.GameBoard.BoardPlayers;
using UnityMvvmToolkit.Core.Interfaces;
using Zenject;

namespace TicTacToe.Gameplay.Factories
{
    public class BoardControllerFactory
    {
        private readonly IInstantiator _instantiator;

        public BoardControllerFactory(IInstantiator instantiator) => 
            _instantiator = instantiator;

        public TController Create<TController>() where TController : IPlayer => 
            _instantiator.Instantiate<TController>();
    }
}