using System;
using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Gameplay.States;
using TicTacToe.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace TicTacToe.Gameplay
{
    public class GameplayMain : MonoBehaviour
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
            _gameStateMachine.Enter<GameplayInitState>().Forget();
        }
    }
}