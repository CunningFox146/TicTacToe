using System;
using Cysharp.Threading.Tasks;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.GameBoard.BoardPlayers;
using TicTacToe.Services.Interactable;
using UnityEngine;
using Zenject;

namespace TicTacToe.Gameplay.Tile
{
    public class FieldTile : MonoBehaviour, IFieldTile, IInteractable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private IGameBoardService _board;
        private GameTile _tile;

        [Inject]
        private void Constructor(IGameBoardService board)
        {
            _board = board;
        }
        
        private void Awake()
        {
            _spriteRenderer.sprite = null;
        }

        private void OnDestroy()
        {
            _tile.StateChanged -= OnStateChanged;
        }
        
        public void Interact()
        {
            if (_board.CurrentPlayer is ISettableTurn player)
                player.SetTurn(_tile.Position);
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
}