using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace TicTacToe.Infrastructure
{
    public class Main : MonoBehaviour
    {
        private IStateMachine _gameStateMachine;
        private StateFactory _stateFactory;

        [Inject]
        private void Construct(IStateMachine gameStateMachine, StateFactory statesFactory)
        {
            _gameStateMachine = gameStateMachine;
            _stateFactory = statesFactory;
        }
        
        private void Start()
        {
            _gameStateMachine.RegisterState(_stateFactory.Create<GameBootstrapState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<GameQuitState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<GameplayLoadState>());
            
            _gameStateMachine.Enter<GameBootstrapState>().Forget();
        }
    }
}