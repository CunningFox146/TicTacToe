using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Tile;
using UnityEngine;

namespace TicTacToe.Gameplay.Field
{
    public interface IGameField
    {
        void SetBackground(Sprite sprite);
        void SetFieldSize(int fieldSize);
        IFieldTile GetTile(Vector2Int position);
        UniTask Init();
    }
}