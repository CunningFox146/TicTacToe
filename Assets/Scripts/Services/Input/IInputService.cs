using System;
using UnityEngine;

namespace TicTacToe.Services.Input
{
    public interface IInputService
    {
        Vector2 PointerPosition { get; }
        event Action Clicked;
    }
}