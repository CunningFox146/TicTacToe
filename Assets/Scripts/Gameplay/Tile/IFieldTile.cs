using TicTacToe.Services.GameBoard;
using UnityEngine;

namespace TicTacToe.Gameplay.Tile
{
    public interface IFieldTile
    {
        void SetPosition(Vector3 position);
        void SetScale(Vector3 scale);
        void SetGameTile(GameTile tile);
    }
}