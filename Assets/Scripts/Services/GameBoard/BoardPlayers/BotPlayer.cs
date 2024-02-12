using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using TicTacToe.Services.GameBoard.Rules;
using UnityEngine;

namespace TicTacToe.Services.GameBoard.BoardPlayers
{
    public class BotPlayer : IPlayer
    {
        private readonly IGameBoardService _board;
        private readonly IGameRules _gameRules;
        private readonly MiniMax _miniMax;

        public Sprite PlayerSprite { get; set; }

        public BotPlayer(IGameBoardService board, IGameRules gameRules)
        {
            _board = board;
            _gameRules = gameRules;
            _miniMax = new MiniMax(gameRules, this);
        }
        
        public UniTask<Vector2Int?> PickTurn(CancellationToken cancellationToken)
        {
            var otherPlayer = _board.Players.First(p => p != this);
            var board = (GameTile[,])_board.Board.Clone();
            return new UniTask<Vector2Int?>(_miniMax.GetBestTurn(board, otherPlayer));
        }
    }
}