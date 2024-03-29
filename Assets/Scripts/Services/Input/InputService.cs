using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace TicTacToe.Services.Input
{
    public class InputService : IInitializable, IDisposable, IInputService
    {
        public event Action Clicked;
        
        private readonly EventSystem _eventSystem;
        private readonly GameplayInput _input;

        private readonly List<RaycastResult> _rayCastResults = new();
        
        public Vector2 PointerPosition => _input.General.Position.ReadValue<Vector2>();

        public InputService(GameplayInput input, EventSystem eventSystem)
        {
            _input = input;
            _eventSystem = eventSystem;
        }

        public void Dispose()
        {
            _input?.Dispose();
        }

        public void Initialize()
        {
            _input.Enable();
            _input.General.Click.performed += OnClick;
        }


        private void OnClick(InputAction.CallbackContext evt)
        {
            if (!IsOverUI(PointerPosition))
                Clicked?.Invoke();
        }

        private bool IsOverUI(Vector2 position)
        {
            _rayCastResults.Clear();
            var eventDataCurrentPosition = new PointerEventData(_eventSystem)
            {
                position = position
            };
            _eventSystem.RaycastAll(eventDataCurrentPosition, _rayCastResults);
            return _rayCastResults.Count > 0;
        }
    }
}