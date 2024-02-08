using UnityEngine;

namespace TicTacToe.UI.Views
{
    public interface IView
    {
        RectTransform Transform { get; }
        void Show();
        void Hide();
        void Destroy();
    }
}