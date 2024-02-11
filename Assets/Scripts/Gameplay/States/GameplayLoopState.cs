using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.States;
using TicTacToe.Services.GameBoard;
using UnityEngine;

namespace TicTacToe.Gameplay.States
{
    public class GameplayLoopState : IState
    {
        private readonly IGameBoardService _board;

        public GameplayLoopState(IGameBoardService board)
        {
            _board = board;
        }
        public async UniTask Enter()
        {
            while(!_board.IsTie() && _board.GetWinner() is null)
               await _board.PickTurn();
            
            if (_board.IsTie())
            {
                // TODO: Tie state
            }
            else if (_board.GetWinner() is not null)
            {
                // TODO: Win state
            }
        }
    }
}