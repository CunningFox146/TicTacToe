using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Factories;
using UnityEngine;
using Zenject;

namespace TicTacToe.Gameplay.Field
{
    public class GameField : MonoBehaviour, IGameField
    {
        [SerializeField] private SpriteRenderer _bgSpriteRenderer;
        [SerializeField] private float _fieldWidth = 3f;
        private IGameplayFactory _gameplayFactory;
        private int _fieldSize = 2;

        [Inject]
        private void Constructor(IGameplayFactory gameplayFactory)
        {
            _gameplayFactory = gameplayFactory;
        }
        
        public void SetBackground(Sprite sprite)
            => _bgSpriteRenderer.sprite = sprite;

        public void SetFieldSize(int fieldSize)
            => _fieldSize = fieldSize;
        
        public async UniTask Init()
        {
            var size = _fieldSize - 1;
            var spacing = 2f / _fieldSize;
            
            for (var x = 0; x < size; x++)
            {
                var offset = Vector3.up * _fieldWidth * spacing * ((-size + 1) * 0.5f + x);
                await CreateLine(offset + Vector3.left * _fieldWidth,  offset + Vector3.right * _fieldWidth);
            }
            
            for (var y = 0; y < size; y++)
            {
                var offset = Vector3.left * _fieldWidth * spacing * ((-size + 1) * 0.5f + y);
                await CreateLine(offset + Vector3.down * _fieldWidth,  offset + Vector3.up * _fieldWidth);
            }

            var startOffset = -(_fieldSize + 1f) * 0.5f;
            var scale = _fieldWidth * 2 / _fieldSize;
            if (_fieldSize > 3)
                startOffset += 1f - _fieldWidth / _fieldSize;
            
            for (var x = 0; x < _fieldSize; x++)
            for (var y = 0; y < _fieldSize; y++)
            {
                var posX = (2 * _fieldWidth * x) / _fieldSize;
                var posY = (2 * _fieldWidth * y) / _fieldSize;
                
                await CreateTile(new Vector3(startOffset + posX, startOffset + posY), scale);
            }
        }

        private async UniTask CreateTile(Vector3 pos, float scale)
        {
            var tile = await _gameplayFactory.CreateFieldTile();
            ((MonoBehaviour)tile).transform.position = pos;
            ((MonoBehaviour)tile).transform.localScale = Vector3.one * scale;
        }

        private async Task CreateLine(Vector3 start, Vector3 end)
        {
            var line = await _gameplayFactory.CreateFieldLine();
            line.SetPositions(new [] {start, end});
        }

        public class Factory : PlaceholderFactory<string, string, UniTask<GameField>>
        {
        }
    }
}