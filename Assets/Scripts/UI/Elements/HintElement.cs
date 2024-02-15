using System;
using DG.Tweening;
using UnityEngine;

namespace TicTacToe.UI.Elements
{
    public class HintElement : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private Sequence _anim; 
            
        private void OnEnable()
        {
            PlayAnim();
        }

        private void OnDisable()
        {
            _anim?.Kill();
        }

        private void PlayAnim()
        {
            _anim?.Kill();

            var moveDirection = new Vector2(-50f, 50f);
            var iconTransform = (RectTransform)_canvasGroup.transform;
            var startPos = iconTransform.anchoredPosition;
            
            _canvasGroup.alpha = 0;

            _anim = DOTween.Sequence()
                .Join(_canvasGroup.DOFade(1f, 0.25f).SetEase(Ease.OutSine))
                .Join(iconTransform.DOAnchorPos(startPos + moveDirection, 0.5f)
                    .SetEase(Ease.InSine)
                    .SetLoops(10, LoopType.Yoyo))
                .OnKill(() => iconTransform.anchoredPosition = startPos);
            
        }
    }
}