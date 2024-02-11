using Cysharp.Threading.Tasks;
using TicTacToe.Services.GameBoard;
using UnityEngine;
using Zenject;

namespace TicTacToe.Gameplay.Tile
{
    public class FieldTile : MonoBehaviour, IFieldTile
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private GameTile _tile;

        private void OnDestroy()
        {
            _tile.StateChanged -= OnStateChanged;
        }

        public void SetPosition(Vector3 position)
            => transform.position = position;

        public void SetScale(Vector3 scale)
            => transform.localScale = scale;

        public void SetGameTile(GameTile tile)
        {
            _tile = tile;
            _tile.StateChanged += OnStateChanged;
        }
        
        private void OnStateChanged()
        {
            _spriteRenderer.sprite = _tile.Player?.PlayerSprite;
        }
        
        public class Factory : PlaceholderFactory<string, string, UniTask<FieldTile>>
        {
        }

    }

    public interface IFieldTile
    {
        void SetPosition(Vector3 position);
        void SetScale(Vector3 scale);
        void SetGameTile(GameTile tile);
    }
}