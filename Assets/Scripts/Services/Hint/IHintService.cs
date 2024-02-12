using TicTacToe.Services.GameBoard;
using TicTacToe.Services.GameBoard.BoardPlayers;
using UnityEngine;

namespace TicTacToe.Services.Hint
{
    public interface IHintService
    {
        Vector2Int GetBestMove(GameTile[,] board, IPlayer player, IPlayer other);
    }
}