using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TicTacToe.Services.Log;

namespace TicTacToe.Infrastructure.States
{
    public class StateMachine : IStateMachine
    {
        private readonly ILogService _log;
        private readonly Dictionary<Type, IState> _registeredStates = new();
        private IState _currentState;

        public StateMachine(ILogService log)
        {
            _log = log;
        }

        public async UniTask Enter<TState>() where TState : class, IState
        {
            var newState = await ChangeState<TState>();
            await newState.Enter();
            _log.Log($"Enter state: {newState.GetType().Name}");
        }

        public void RegisterState<TState>(TState state) where TState : class, IState
        {
            if (_registeredStates.ContainsKey(typeof(TState)))
                _registeredStates[typeof(TState)] = state;
            else
                _registeredStates.Add(typeof(TState), state);
        }

        private async UniTask<TState> ChangeState<TState>() where TState : class, IState
        {
            if(_currentState is IStateExitable exitable)
                await exitable.Exit();
      
            var state = GetState<TState>();
            _currentState = state;
      
            return state;
        }
    
        private TState GetState<TState>() where TState : class, IState => 
            _registeredStates[typeof(TState)] as TState;
    }
}