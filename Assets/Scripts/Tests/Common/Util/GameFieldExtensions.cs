using TicTacToe.Gameplay.GameBoard;
using UnityEngine;

namespace TicTacToe.Tests.Common.Util
{
    public static class GameFieldExtensions
    {
        public static int GetOccupiedTiles(this IGameBoardController boardController, int size)
        {
            var occupiedCount = 0;
            for (var x = 0; x < size; x++)
            for (var y = 0; y < size; y++)
                if (boardController.GetTile(new Vector2Int(x, y)).IsOccupied)
                    occupiedCount++;

            return occupiedCount;
        }
    }
}