using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.Gameplay.Line
{
    public interface IFieldLine
    {
        void SetTexture(Texture texture);
        void SetPositions(Vector3[] positions);
    }
}