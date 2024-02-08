using TicTacToe.UI.ViewStack;
using UnityEngine;

namespace TicTacToe.UI.Views
{
    public interface IView
    {
        void Show();
        void Hide();
        void Destroy();
    }
}