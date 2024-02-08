using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace TicTacToe.UI.Views
{
    public class LoadingCurtainView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeTime = 1f;

        public void Hide()
        {
            _canvasGroup.DOFade(0f, _fadeTime)
                .OnComplete(() => Destroy(gameObject));
        }
        
        public class Factory : PlaceholderFactory<string, string, UniTask<LoadingCurtainView>>
        {
        }
    }
}