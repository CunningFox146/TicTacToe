using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace TicTacToe.UI.Elements
{
    public class CountdownElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private Sequence _anim;
        private string _oldText;
        
        private RectTransform Target => (RectTransform)_text.transform;

        private void OnDisable()
        {
            if (!Target)
                return;
            Target.localEulerAngles = Vector3.zero;
            Target.localScale = Vector3.one;
            _anim?.Kill();
        }

        private void Update()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            if (_text.text == _oldText)
                return;
            _oldText = _text.text;
            PlayAnim();
        }

        private void PlayAnim()
        {
            if (!Target)
                return;
            
            Target.localScale = Vector3.zero;

            _anim = DOTween.Sequence()
                .Join(Target.DORotate(Vector3.forward * 360f, 0.25f, RotateMode.FastBeyond360))
                .Join(Target.DOScale(Vector3.one, 0.25f))
                .Append(Target.DORotate(Vector3.back * 360f, 0.25f, RotateMode.FastBeyond360).SetDelay(0.5f))
                .Join(Target.DOScale(Vector3.zero, 0.25f));
        }
    }
}