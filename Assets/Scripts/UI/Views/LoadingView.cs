using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace TicTacToe.UI.Views
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public void Hide()
        {
            _canvasGroup.DOFade(0f, 0.25f)
                .OnComplete(() => Destroy(gameObject));
        }
        
        public class Factory : PlaceholderFactory<string, string, UniTask<LoadingView>>
        {
        }
    }
}