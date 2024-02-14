using UnityEngine;

namespace TicTacToe.Services.BoardPlayers
{
    public interface ISettableMove
    {
        void SetMove(Vector2Int turn);
    }
}