using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TicTacToe.Services.GameBoard.Controllers
{
    public interface IBoardController
    {
        UniTask<Vector2Int> GetTurn(CancellationToken cancellationToken);
    }
}