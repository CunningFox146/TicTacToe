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
            while(!_board.IsTie() && _board.GetWinner(out _) is null)
               await _board.PickTurn();
            
            if (_board.IsTie())
            {
                Debug.Log("TIE");
            }
            else if (_board.GetWinner(out _) is not null)
            {
                Debug.Log("WIN");
            }
        }
    }
}