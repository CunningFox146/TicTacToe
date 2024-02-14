using System.Collections.Generic;
using TicTacToe.Services.BoardPlayers;
using TicTacToe.Services.GameBoard;
using UnityEngine;

namespace TicTacToe.Tests.Common.Util
{
    public static class GameBoardExtensions
    {
        public static void FillBoardRandomly(this GameBoardService board, IReadOnlyList<IPlayer> players = null)
        {
            players ??= board.Players;
            var boardSize = board.Board.GetLength(0);
            for (var x = 0; x < boardSize; x++)
            for (var y = 0; y < boardSize; y++)
                board.AddMoveCommand(players[Random.Range(0, players.Count)], new Vector2Int(x, y));
        }
        
        public static void FillBoard(this GameBoardService board, IReadOnlyList<int> tiles, IReadOnlyList<IPlayer> players = null)
        {
            players ??= board.Players;
            var boardSize = board.Board.GetLength(0);
            for (var i = 0; i < tiles.Count; i++)
            {
                var x = i / boardSize;
                var y = i % boardSize;

                board.AddMoveCommand(players[tiles[i]], new Vector2Int(x, y));
            }
        }

        public static int GetOccupiedTiles(this GameBoardService board)
        {
            var occupied = 0;
            var boardSize = board.Board.GetLength(0);
            for (var x = 0; x < boardSize; x++)
            for (var y = 0; y < boardSize; y++)
                if (board.Board[x, y].IsOccupied)
                    occupied++;
            
            return occupied;
        }
    }
}