using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.States;
using TicTacToe.Services.GameBoard;

namespace TicTacToe.Gameplay.States
{
    public class GameplayLoopState : IState, IStateExitable, IDisposable
    {
        private readonly IGameBoardService _board;
        private readonly IStateMachine _gameStateMachine;
        private CancellationTokenSource _cancellationTokenSource;

        public GameplayLoopState(IGameBoardService board, IStateMachine gameStateMachine)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _board = board;
            _gameStateMachine = gameStateMachine;
        }
        
        public async UniTask Enter()
        {
            ResetCancellationToken();
            
            await _board.PickMove(_cancellationTokenSource.Token);

            if (_cancellationTokenSource.Token.IsCancellationRequested)
                return;
            
            if (_board.IsTie() || _board.GetWinner() is not null)
                await _gameStateMachine.Enter<GameEndState>();
            else
                await _gameStateMachine.Enter<GameplayLoopState>();
        }

        public UniTask Exit()
        {
            _cancellationTokenSource?.Cancel();
            return UniTask.CompletedTask;
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }

        private void ResetCancellationToken()
        {
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }
    }
}