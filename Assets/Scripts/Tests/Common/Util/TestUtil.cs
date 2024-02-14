using System.Collections.Generic;
using System.Linq;
using NSubstitute;
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
    }
}