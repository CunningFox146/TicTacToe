using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.GameBoard.BoardPlayers;
using TicTacToe.Services.GameBoard.Rules;
using TicTacToe.Services.Randomizer;
using UnityEngine;

namespace TicTacToe.Services.Hint
{
    public class HintService : IHintService
    {
        private readonly IRandomService _randomService;
        private readonly MiniMax _miniMax;

        public HintService(IGameRules gameRules, IRandomService randomService)
        {
            _miniMax = new MiniMax(gameRules);
            _randomService = randomService;
        }

        public async UniTask<Vector2Int?> GetBestMove(GameTile[,] board, IPlayer player, IPlayer other)
        {
            var task = Task<Vector2Int?>.Factory.StartNew(() => GetBestMoveSync(board, player, other));
            return await task.AsUniTask();
        }

        public Vector2Int? GetBestMoveSync(GameTile[,] sourceBoard, IPlayer player, IPlayer other)
        {
            var freeTiles = 0;
            foreach (var tile in sourceBoard)
            {
                if (!tile.IsOccupied)
                    freeTiles++;
            }

            switch (freeTiles)
            {
                case 0:
                    return null;
                case > 9:
                    return GetRandomTile(sourceBoard);
                default:
                {
                    var board = CloneBoard(sourceBoard);
                    return _miniMax.GetBestMove(board, player, other);
                }
            }
        }

        private Vector2Int GetRandomTile(GameTile[,] board)
        {
            var freeTiles = new List<GameTile>();
            foreach (var tile in board)
            {
                if (!tile.IsOccupied)
                    freeTiles.Add(tile);
            }

            return freeTiles[_randomService.GetInRange(0, freeTiles.Count - 1)].Position;
        }

        private static GameTile[,] CloneBoard(GameTile[,] sourceBoard)
        {
            var size = sourceBoard.GetLength(0);
            var board = new GameTile[size, size];
            for (var x = 0; x < size; x++)
            for (var y = 0; y < size; y++)
                board[x, y] = (GameTile)sourceBoard[x, y].Clone();
            
            return board;
        }
    }
}