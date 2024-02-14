using System;
using TicTacToe.Services.Input;
using TicTacToe.Util;
using UnityEngine;
using Zenject;

namespace TicTacToe.Services.Interactable
{
    public class InteractionService : IInitializable, IDisposable
    {
        private readonly Camera _camera;
        private readonly IInputService _input;
        private readonly int _layerMask;

        public InteractionService(IInputService input, Camera camera)
        {
            _layerMask = 1 << (int)Layers.Interactable;
            _input = input;
            _camera = camera;
        }

        public void Dispose()
        {
            _input.Clicked -= OnClick;
        }

        public void Initialize()
        {
            _input.Clicked += OnClick;
        }

        private IInteractable GetInteractableAtPoint(Vector2 pos)
        {
            var point = new Vector3(pos.x, pos.y, _camera.nearClipPlane);
            var collider = Physics2D.OverlapPoint(_camera.ScreenToWorldPoint(point), _layerMask);
            return collider ? collider.GetComponentInParent<IInteractable>() : null;
        }

        private void OnClick() 
            => GetInteractableAtPoint(_input.PointerPosition)?.Interact();
    }
}