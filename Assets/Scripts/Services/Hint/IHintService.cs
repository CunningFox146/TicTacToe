using Cysharp.Threading.Tasks;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.GameBoard.BoardPlayers;
using UnityEngine;

namespace TicTacToe.Services.Hint
{
    public interface IHintService
    {
        UniTask<Vector2Int?> GetBestMove(GameTile[,] board, IPlayer player, IPlayer other);
    }
}