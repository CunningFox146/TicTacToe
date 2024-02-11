using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TicTacToe.Services.GameBoard.BoardPlayers
{
    public interface IPlayer
    {
        Sprite PlayerSprite { get; }
        UniTask<Vector2Int> PickTurn(CancellationToken cancellationToken);
    }
}