using Cysharp.Threading.Tasks;
using TicTacToe.Services.BoardPlayers;
using TicTacToe.Services.GameBoard;
using UnityEngine;

namespace TicTacToe.Services.Hint
{
    public interface IHintService
    {
        UniTask<Vector2Int?> GetBestMove(GameTile[,] board, IPlayer player, IPlayer other);
    }
}