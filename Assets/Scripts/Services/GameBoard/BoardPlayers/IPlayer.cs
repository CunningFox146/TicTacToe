using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TicTacToe.Services.GameBoard.BoardPlayers
{
    public interface IPlayer
    {
        Sprite PlayerSprite { get; set; }
        UniTask<Vector2Int?> PickTurn(CancellationToken cancellationToken);
    }
}