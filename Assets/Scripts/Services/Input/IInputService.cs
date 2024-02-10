using System;
using UnityEngine;

namespace TicTacToe.Services.Input
{
    public interface IInputService
    {
        event Action Clicked;
        Vector2 PointerPosition { get; }
    }
}