using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TicTacToe.Services.GameBoard.BoardPlayers
{
    public class Player : IPlayer, ISettableTurn
    {
        public Sprite PlayerSprite { get; set; }
        private Vector2Int? _pickedTurn;
        
        public async UniTask<Vector2Int?> PickTurn(CancellationToken cancellationToken)
        {
            while (_pickedTurn is null && !cancellationToken.IsCancellationRequested)
                await UniTask.Yield();

            var turn = _pickedTurn;
            _pickedTurn = null;
            return turn;
        }

        public void SetTurn(Vector2Int turn) 
            => _pickedTurn = turn;
    }
}