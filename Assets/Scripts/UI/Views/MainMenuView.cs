using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TicTacToe.UI.ViewModels;
using UnityEngine;
using Zenject;

namespace TicTacToe.UI.Views
{
    public class MainMenuView : ViewBase<MainMenuViewModel>
    {
        [SerializeField] private RectTransform _header;
        [SerializeField] private CanvasGroup _menu;
        [SerializeField] private float _animDuration = 1f;

        private void Start()
        {
            var menuTransform = (RectTransform)_menu.transform;
            var startHeaderPos = _header.anchoredPosition;
            var startMenuPos = menuTransform.anchoredPosition;

            _menu.alpha = 0f;
            _header.localScale = Vector3.one * 2f;
            _header.anchoredPosition += Vector2.down * 500f;
            menuTransform.anchoredPosition += Vector2.down * 100f;

            DOTween.Sequence()
                .Join(_header.DOScale(Vector3.one, _animDuration).SetEase(Ease.OutBack))
                .Join(_header.DOAnchorPos(startHeaderPos, _animDuration).SetEase(Ease.OutSine))
                .Append(menuTransform.DOAnchorPos(startMenuPos, _animDuration).SetEase(Ease.OutSine))
                .Join(_menu.DOFade(1f, _animDuration).SetEase(Ease.OutSine));
        }

        public class Factory : PlaceholderFactory<string, string, UniTask<MainMenuView>>
        {
        }
    }
}