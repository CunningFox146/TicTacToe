using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using TicTacToe.Services.Hint;
using UnityEngine;

namespace TicTacToe.Services.GameBoard.BoardPlayers
{
    public class BotPlayer : IPlayer
    {
        private readonly IGameBoardService _board;
        private readonly IHintService _hintService;
        
        public string Name { get; set; }
        public Sprite PlayerSprite { get; set; }

        public BotPlayer(IGameBoardService board, IHintService hintService)
        {
            _board = board;
            _hintService = hintService;
        }
        
        public async UniTask<Vector2Int?> PickMove(CancellationToken cancellationToken)
        {
            var otherPlayer = _board.Players.First(p => p != this);
            return await _hintService.GetBestMove(_board.Board, this, otherPlayer);
        }
    }
}