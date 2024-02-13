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

        public async UniTask<Vector2Int> GetBestMove(GameTile[,] sourceBoard, IPlayer player, IPlayer other)
        {
            var freeTiles = 0;
            foreach (var tile in sourceBoard)
            {
                if (!tile.IsOccupied)
                    freeTiles++;
            }

            if (freeTiles > 9)
                return GetRandomTile(sourceBoard);
                
            
            var board = CloneBoard(sourceBoard);
            var task = Task<Vector2Int>.Factory.StartNew(() => _miniMax.GetBestMove(board, player, other));
            await task.AsUniTask();
            return task.Result;
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