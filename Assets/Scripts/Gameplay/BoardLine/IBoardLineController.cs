using UnityEngine;

namespace TicTacToe.Gameplay.BoardLine
{
    public interface IBoardLineController
    {
        void SetTexture(Texture texture);
        void SetPositions(Vector3[] positions);
    }
}