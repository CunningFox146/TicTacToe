using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Line;
using UnityEngine;
using Zenject;

namespace TicTacToe.Gameplay.Tile
{
    public class FieldTile : MonoBehaviour, IFieldTile
    {
        public class Factory : PlaceholderFactory<string, string, UniTask<FieldTile>>
        {
        }
    }

    public interface IFieldTile
    {
    }
}