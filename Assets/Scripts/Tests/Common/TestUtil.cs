using System.Collections.Generic;
using NSubstitute;
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
        
        public static void FillBoard(IReadOnlyList<int> tiles, GameBoardService board, IReadOnlyList<IPlayer> players)
        {
            var boardSize = board.Board.GetLength(0);
            for (var i = 0; i < tiles.Count; i++)
            {
                var x = i / boardSize;
                var y = i % boardSize;

                board.AddMoveCommand(new Vector2Int(x, y), players[tiles[i]]);
            }
        }
    }
}