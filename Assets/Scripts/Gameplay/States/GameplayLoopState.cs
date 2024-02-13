using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.States;
using TicTacToe.Services.GameBoard;
using UnityEngine;

namespace TicTacToe.Gameplay.States
{
    public class GameplayLoopState : IState
    {
        private readonly IGameBoardService _board;
        private readonly IStateMachine _gameStateMachine;

        public GameplayLoopState(IGameBoardService board, IStateMachine gameStateMachine)
        {
            _board = board;
            _gameStateMachine = gameStateMachine;
        }
        public async UniTask Enter()
        {
            while(!_board.IsTie() && _board.GetWinner(out _) is null)
               await _board.PickTurn();
            
            if (_board.IsTie())
            {
                await _gameStateMachine.Enter<GameTieState>();
            }
            else if (_board.GetWinner(out _) is not null)
            {
                await _gameStateMachine.Enter<GameWonState>();
            }
        }
    }
}