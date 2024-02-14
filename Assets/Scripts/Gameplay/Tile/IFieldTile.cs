using TicTacToe.Services.GameBoard;
using UnityEngine;

namespace TicTacToe.Gameplay.Tile
{
    public interface IFieldTile
    {
        bool IsOccupied { get; }
        void SetPosition(Vector3 position);
        void SetScale(Vector3 scale);
        void SetGameTile(GameTile tile);
        Vector3 GetScreenPosition(Camera mainCamera);
    }
}