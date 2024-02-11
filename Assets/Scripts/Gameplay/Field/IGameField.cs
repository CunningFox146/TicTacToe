using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TicTacToe.Gameplay.Field
{
    public interface IGameField
    {
        void SetBackground(Sprite sprite);
        UniTask Init();
    }
}