using System.Collections.Generic;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.Randomizer;
using UnityEngine;

namespace TicTacToe.Util
{
    public static class BoardExtensions
    {
        public static int GetBoardSize(this GameTile[,] board) 
            => board.GetLength(0);

        public static int GetOccupiedTilesCount(this GameTile[,] tiles)
        {
            var freeTiles = 0;
            foreach (var tile in tiles)
                if (tile.IsOccupied)
                    freeTiles++;
            return freeTiles;
        }

        public static int GetFreeTilesCount(this GameTile[,] tiles) 
            => tiles.Length - GetOccupiedTilesCount(tiles);

        public static Vector2Int GetRandomTile(this IRandomService randomService, GameTile[,] board)
        {
            var freeTiles = GetFreeTiles(board);
            return freeTiles[randomService.GetInRange(0, freeTiles.Count - 1)].Position;
        }

        public static GameTile[,] CloneBoard(this GameTile[,] sourceBoard)
        {
            var size = sourceBoard.GetLength(0);
            var board = new GameTile[size, size];
            for (var x = 0; x < size; x++)
            for (var y = 0; y < size; y++)
                board[x, y] = (GameTile)sourceBoard[x, y].Clone();

            return board;
        }

        public static Vector2Int? GetFirstFreeTile(this GameTile[,] board)
        {
            var boardSize = GetBoardSize(board);

            for (var x = 0; x < boardSize; x++)
            for (var y = 0; y < boardSize; y++)
                if (!board[x, y].IsOccupied)
                    return new Vector2Int(x, y);

            return null;
        }

        private static List<GameTile> GetFreeTiles(GameTile[,] board)
        {
            var freeTiles = new List<GameTile>();
            foreach (var tile in board)
                if (!tile.IsOccupied)
                    freeTiles.Add(tile);
            return freeTiles;
        }
    }
}