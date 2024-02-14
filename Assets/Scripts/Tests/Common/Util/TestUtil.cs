using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.GameBoard.BoardPlayers;
using UnityEngine;

namespace TicTacToe.Tests.Common.Util
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

        public static T GetObjectByName<T>(this Component root, string name) where T : Component
        {
            var buttons = root.GetComponentsInChildren<T>(true);
            var undoButton = buttons.First(b => b.name == name);
            return undoButton;
        }
        
        public static GameTile[,] GetMockBoard(int boardSize)
        {
            var board = new GameTile[boardSize, boardSize];
            for (var x = 0; x < boardSize; x++)
            for (var y = 0; y < boardSize; y++)
            {
                board[x, y] = new GameTile(new Vector2Int(x, y));
            }

            return board;
        }
    }
}