using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.BoardTile;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Services.GameBoard;
using UnityEngine;
using Zenject;

namespace TicTacToe.Gameplay.GameBoard
{
    public class GameBoardController : MonoBehaviour, IGameBoardController
    {
        [SerializeField] private SpriteRenderer _bgSpriteRenderer;
        [SerializeField] private float _fieldWidth = 3f;

        private IBoardTileController[,] _tiles;
        private IGameplayFactory _gameplayFactory;
        private IGameBoardService _gameBoard;
        private int _fieldSize = 2;

        [Inject]
        private void Constructor(IGameplayFactory gameplayFactory, IGameBoardService gameBoard)
        {
            _gameBoard = gameBoard;
            _gameplayFactory = gameplayFactory;
        }
        
        public void SetBackground(Sprite sprite)
            => _bgSpriteRenderer.sprite = sprite;

        public void SetBoardSize(int fieldSize)
            => _fieldSize = fieldSize;

        public IBoardTileController GetTile(Vector2Int position)
            => _tiles[position.x, position.y];


        public async UniTask Init()
        {
            await CreateLines();
            await CreateTiles();
        }

        private async UniTask CreateTiles()
        {
            _tiles = new IBoardTileController[_fieldSize, _fieldSize];
            var startOffset = -(_fieldSize + 1f) * 0.5f;
            var scale = _fieldWidth * 2 / _fieldSize;
            if (_fieldSize > 3)
                startOffset += 1f - _fieldWidth / _fieldSize;
            
            for (var x = 0; x < _fieldSize; x++)
            for (var y = 0; y < _fieldSize; y++)
            {
                var posX = startOffset + (2 * _fieldWidth * x) / _fieldSize;
                var posY = startOffset + (2 * _fieldWidth * y) / _fieldSize;
                var tile = _gameBoard.Board[x, y];
                
                _tiles[x, y] = await CreateTile(new Vector3(posX, posY), scale, tile);
            }
        }

        private async UniTask CreateLines()
        {
            var size = _fieldSize - 1;
            var spacing = 2f / _fieldSize;
            
            for (var x = 0; x < size; x++)
            {
                var offset = Vector3.up * _fieldWidth * spacing * ((-size + 1) * 0.5f + x);
                var start = offset + Vector3.left * _fieldWidth;
                var end = offset + Vector3.right * _fieldWidth;
                
                await CreateLine(start,  end);
            }
            
            for (var y = 0; y < size; y++)
            {
                var offset = Vector3.left * _fieldWidth * spacing * ((-size + 1) * 0.5f + y);
                var start = offset + Vector3.down * _fieldWidth;
                var end = offset + Vector3.up * _fieldWidth;
                
                await CreateLine(start, end);
            }
        }

        private async UniTask<IBoardTileController> CreateTile(Vector3 position, float scale, GameTile gameTile)
        {
            var tile = await _gameplayFactory.CreateBoardTileController();
            tile.SetPosition(position);
            tile.SetScale(Vector3.one * scale);
            tile.SetGameTile(gameTile);
            return tile;
        }

        private async Task CreateLine(Vector3 start, Vector3 end)
        {
            var line = await _gameplayFactory.CreateBoardLineController();
            line.SetPositions(new [] {start, end});
        }

        public class Factory : PlaceholderFactory<string, string, UniTask<GameBoardController>>
        {
        }
    }
}