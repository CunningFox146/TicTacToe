using UnityEngine;

namespace TicTacToe.Services.GameBoard.BoardPlayers
{
    public interface ISettableTurn
    {
        void SetTurn(Vector2Int turn);
    }
}