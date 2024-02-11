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
        private IGameplayFactory _gameplayFactory;
        private int _fieldSize = 3;
        private float _fieldWidth = 3f;


        [Inject]
        private void Constructor(IGameplayFactory gameplayFactory)
        {
            _gameplayFactory = gameplayFactory;
        }
        
        public void SetBackground(Sprite sprite)
            => _bgSpriteRenderer.sprite = sprite;

        public async UniTask Init()
        {
            var size = _fieldSize - 1;
            for (var x = 0; x < size; x++)
            {
                var offset = Vector3.up * _fieldWidth * (2f / _fieldSize) * ((-size + 1) * 0.5f + x);
                await CreateLine(offset + Vector3.left * _fieldWidth,  offset + Vector3.right * _fieldWidth);
            }
            
            for (var y = 0; y < size; y++)
            {
                var offset = Vector3.left * _fieldWidth * (2f / _fieldSize) *  ((-size + 1) * 0.5f + y);
                await CreateLine(offset + Vector3.down * _fieldWidth,  offset + Vector3.up * _fieldWidth);
            }
            
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