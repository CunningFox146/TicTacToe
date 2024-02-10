using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TicTacToe.Gameplay.Field
{
    public class GameField : MonoBehaviour, IGameField
    {
        [SerializeField] private SpriteRenderer _bgSpriteRenderer;

        public void SetSprite(Sprite sprite)
            => _bgSpriteRenderer.sprite = sprite;
        
        public class Factory : PlaceholderFactory<string, string, UniTask<GameField>>
        {
        }
    }
}