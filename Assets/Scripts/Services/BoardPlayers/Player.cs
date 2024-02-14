using System.Threading;
using Cysharp.Threading.Tasks;
using TicTacToe.Services.GameBoard;
using UnityEngine;

namespace TicTacToe.Services.BoardPlayers
{
    public class Player : IPlayer, ISettableMove
    {
        private readonly IGameBoardService _gameBoard;
        public Sprite PlayerSprite { get; set; }
        private Vector2Int? _pickedTurn;
        
        public string Name { get; set; }

        public Player(IGameBoardService gameBoard)
        {
            _gameBoard = gameBoard;
        }
        
        public async UniTask<Vector2Int?> PickMove(CancellationToken cancellationToken)
        {
            while (_pickedTurn is null && !cancellationToken.IsCancellationRequested)
                await UniTask.Yield();

            var turn = _pickedTurn;
            _pickedTurn = null;
            return turn;
        }

        public void SetMove(Vector2Int turn)
        {
            if (!_gameBoard.Board[turn.x, turn.y].IsOccupied)
                _pickedTurn = turn;
        }
    }
}