using UnityEngine;
using UnityEngine.EventSystems;

namespace TicTacToe.UI.Elements
{
    public class ButtonElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _textTransform;
        [SerializeField] private float _textPressedOffset;

        public void OnPointerDown(PointerEventData eventData)
        {
            ApplyTextOffset();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            RemoveTextOffset();
        }

        private void RemoveTextOffset()
        {
            _textTransform.anchoredPosition -= Vector2.up * _textPressedOffset;
        }

        private void ApplyTextOffset()
        {
            _textTransform.anchoredPosition += Vector2.up * _textPressedOffset;
        }
    }
}