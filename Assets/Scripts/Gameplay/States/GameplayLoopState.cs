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
            await _board.PickTurn();
            for (int x = 0; x < _board.BoardSize; x++)
            for (int y = 0; y < _board.BoardSize; y++)
                Debug.Log($"[{x},{y}] = {_board.GetTile(x, y).IsOccupied}");
            
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