using Cysharp.Threading.Tasks;
using TicTacToe.UI.Views;
using UnityEngine;
using Zenject;

namespace TicTacToe.Gameplay.GameplayBg
{
    public class GameplayBackground : MonoBehaviour, IGameplayBackground
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetSprite(Sprite sprite)
            => _spriteRenderer.sprite = sprite;
        
        public class Factory : PlaceholderFactory<string, string, UniTask<GameplayBackground>>
        {
        }
    }
}