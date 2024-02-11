using TicTacToe.Services.GameBoard.Controllers;
using UnityMvvmToolkit.Core.Interfaces;
using Zenject;

namespace TicTacToe.Gameplay.Factories
{
    public class BoardControllerFactory
    {
        private readonly IInstantiator _instantiator;

        public BoardControllerFactory(IInstantiator instantiator) => 
            _instantiator = instantiator;

        public TController Create<TController>() where TController : IBoardController => 
            _instantiator.Instantiate<TController>();
    }
}