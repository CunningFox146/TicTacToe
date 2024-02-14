using TicTacToe.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace TicTacToe.CompositionRoot
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        private IStateMachine _stateMachine;
        private StateFactory _stateFactory;

        [Inject]
        private void Constructor(IStateMachine stateMachine, StateFactory stateFactory)
        {
            _stateFactory = stateFactory;
            _stateMachine = stateMachine;
        }

        private void Start()
        {
            _stateMachine.RegisterState(_stateFactory.Create<MainMenuState>());
            _stateMachine.Enter<MainMenuState>();
        }
    }
}