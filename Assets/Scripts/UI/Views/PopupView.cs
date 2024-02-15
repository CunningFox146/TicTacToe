using DG.Tweening;
using UnityEngine;

namespace TicTacToe.UI.Views
{
    public class PopupView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _root;
        [SerializeField] private float _animDuration = 0.5f;

        private void Start()
        {
            var startPos = _root.anchoredPosition;
            _canvasGroup.alpha = 0f;
            _root.anchoredPosition += Vector2.up * 150;

            DOTween.Sequence()
                .Join(_canvasGroup.DOFade(1f, _animDuration).SetEase(Ease.OutSine))
                .Join(_root.DOAnchorPos(startPos, _animDuration).SetEase(Ease.OutSine));
        }
    }
}