using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TicTacToe.Services.GameBoard.BoardPlayers
{
    public interface IPlayer
    {
        string Name { get; set; }
        Sprite PlayerSprite { get; set; }
        UniTask<Vector2Int?> PickMove(CancellationToken cancellationToken);
    }
}