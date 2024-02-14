using System.Collections.Generic;
using NSubstitute;
using TicTacToe.Gameplay.Field;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.GameBoard.BoardPlayers;
using UnityEngine;

namespace TicTacToe.Tests.Common
{
    public static class TestUtil
    {
        public static IReadOnlyList<IPlayer> GetSubstitutePlayers()
        {
            var playerX = Substitute.For<IPlayer>();
            playerX.Name = "X";
            
            var playerO = Substitute.For<IPlayer>();
            playerO.Name = "O";

            return new[] { playerX, playerO };
        }

        public static void FillBoardRandomly(this GameBoardService board, IReadOnlyList<IPlayer> players = null)
        {
            players ??= board.Players;
            var boardSize = board.Board.GetLength(0);
            for (var x = 0; x < boardSize; x++)
            for (var y = 0; y < boardSize; y++)
                board.AddMoveCommand(new Vector2Int(x, y), players[Random.Range(0, players.Count)]);
        }
        
        public static void FillBoard(this GameBoardService board, IReadOnlyList<int> tiles, IReadOnlyList<IPlayer> players = null)
        {
            players ??= board.Players;
            var boardSize = board.Board.GetLength(0);
            for (var i = 0; i < tiles.Count; i++)
            {
                var x = i / boardSize;
                var y = i % boardSize;

                board.AddMoveCommand(new Vector2Int(x, y), players[tiles[i]]);
            }
        }

        public static int GetOccupiedTiles(this IGameField field, int size)
        {
            var occupiedCount = 0;
            for (var x = 0; x < size; x++)
            for (var y = 0; y < size; y++)
            {
                if (field.GetTile(new Vector2Int(x, y)).IsOccupied)
                    occupiedCount++;
            }

            return occupiedCount;
        }
    }
}