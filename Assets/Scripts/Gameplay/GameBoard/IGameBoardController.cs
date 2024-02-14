using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.BoardTile;
using UnityEngine;

namespace TicTacToe.Gameplay.GameBoard
{
    public interface IGameBoardController
    {
        void SetBackground(Sprite sprite);
        void SetBoardSize(int fieldSize);
        IBoardTileController GetTile(Vector2Int position);
        UniTask Init();
    }
}