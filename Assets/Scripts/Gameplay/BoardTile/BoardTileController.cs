using Cysharp.Threading.Tasks;
using DG.Tweening;
using TicTacToe.Services.BoardPlayers;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.Interactable;
using TicTacToe.Services.Sounds;
using UnityEngine;
using Zenject;

namespace TicTacToe.Gameplay.BoardTile
{
    public class BoardTileController : MonoBehaviour, IBoardTileController, IInteractable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private IGameBoardService _board;
        private GameTile _tile;
        private ISoundSource _soundSource;
        private Tween _spawnTween;
        
        public bool IsOccupied => _tile.IsOccupied;

        [Inject]
        private void Constructor(IGameBoardService board, ISoundSource soundSource)
        {
            _soundSource = soundSource;
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
            if (_board.CurrentPlayer is ISettableMove player)
                player.SetMove(_tile.Position);
        }

        public void SetPosition(Vector3 position)
            => transform.position = position;

        public void SetScale(Vector3 scale)
            => transform.localScale = scale;

        public Vector3 GetScreenPosition(Camera mainCamera) 
            => mainCamera.WorldToScreenPoint(transform.position);

        public void SetGameTile(GameTile tile)
        {
            _tile = tile;
            _tile.StateChanged += OnStateChanged;
        }
        
        private void OnStateChanged()
        {
            _spriteRenderer.sprite = _tile.Player?.PlayerSprite;
            if (_tile.IsOccupied)
            {
                PlaySpawnAnim();
                PlayDrawSound();
            }
        }

        private void PlaySpawnAnim()
        {
            _spawnTween?.Kill();
            var spriteTransform = _spriteRenderer.transform;
            spriteTransform.localScale = Vector3.one * 0.5f;
            _spawnTween = spriteTransform.DOScale(Vector3.one, 0.5f)
                .SetEase(Ease.OutBack);
        }

        private void PlayDrawSound() 
            => _soundSource.PlaySound(SoundNames.Draw);

        public class Factory : PlaceholderFactory<string, string, UniTask<BoardTileController>>
        {
        }
    }
}