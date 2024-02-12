using System;
using TicTacToe.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace TicTacToe.Infrastructure
{
    public class MainMenuMain : MonoBehaviour
    {
        private IStateMachine _stateMachine;

        [Inject]
        private void Constructor(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Start()
        {
            _stateMachine.Enter<MainMenuState>();
        }
    }
}