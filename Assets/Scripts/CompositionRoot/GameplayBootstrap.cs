using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.States;
using TicTacToe.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace TicTacToe.CompositionRoot
{
    public class GameplayBootstrap : MonoBehaviour
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
            _gameStateMachine.RegisterState(_stateFactory.Create<GameplayInitState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<GameplayLoopState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<GameEndState>());
            
            _gameStateMachine.Enter<GameplayInitState>().Forget();
        }
    }
}