using System;
using System.Collections.Generic;
using TicTacToe.Services.Input;
using TicTacToe.Util;
using UnityEngine;
using Zenject;

namespace TicTacToe.Services.Interactable
{
    public class InteractionService : IInitializable, IDisposable, IFixedTickable
    {
        private readonly IInputService _input;
        private readonly Camera _camera;
        private readonly int _layerMask;
        private readonly Queue<Vector2> _clickQueue = new();

        public InteractionService(IInputService input, Camera camera)
        {
            _layerMask = 1 << (int)Layers.Interactable;
            _input = input;
            _camera = camera;
        }

        public void Initialize()
        {
            _input.Clicked += OnClick;
        }

        public void Dispose()
        {
            _input.Clicked -= OnClick;
        }

        public void FixedTick()
        {
            while (_clickQueue.TryDequeue(out var pos))
                GetInteractableAtPoint(pos)?.Interact();
        }

        private IInteractable GetInteractableAtPoint(Vector2 pos)
        {
            var point = new Vector3(pos.x, pos.y, _camera.nearClipPlane);
            var collider = Physics2D.OverlapPoint(_camera.ScreenToWorldPoint(point), _layerMask);
            return collider ? collider.GetComponentInParent<IInteractable>() : null;
        }
        
        private void OnClick() 
            => _clickQueue.Enqueue(_input.PointerPosition);
    }
}