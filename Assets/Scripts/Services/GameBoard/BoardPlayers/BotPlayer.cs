using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TicTacToe.Services.GameBoard.BoardPlayers
{
    public class BotPlayer : IPlayer
    {
        private readonly IGameBoardService _board;
        
        public Sprite PlayerSprite { get; set; }

        public BotPlayer(IGameBoardService board)
        {
            _board = board;
        }
        
        public UniTask<Vector2Int> PickTurn(CancellationToken cancellationToken)
        {
            for (var x = 0; x < _board.BoardSize; x++)
            for (var y = 0; y < _board.BoardSize; y++)
            {
                var tile = _board.GetTile(x, y);
                if (!tile.IsOccupied)
                    return new UniTask<Vector2Int>(new Vector2Int(x, y));
            }
            return new UniTask<Vector2Int>(Vector2Int.zero);
        }
    }
}