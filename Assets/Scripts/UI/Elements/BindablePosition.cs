using UnityEngine;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace TicTacToe.UI.Elements
{
    public class BindablePosition : MonoBehaviour, IBindableElement
    {
        [SerializeField] private string _bindingPositionPath;

        private IProperty<Vector3> _positionProperty;
        private PropertyBindingData _propertyBindingData;

        public void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            _propertyBindingData ??= _bindingPositionPath.ToPropertyBindingData();

            _positionProperty = objectProvider.RentProperty<Vector3>(context, _propertyBindingData);
            _positionProperty.ValueChanged += OnPropertyValueChanged;

            SetPosition(_positionProperty.Value);
        }

        public void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_positionProperty is null)
                return;

            _positionProperty.ValueChanged -= OnPropertyValueChanged;
            objectProvider.ReturnReadOnlyProperty(_positionProperty);
            _positionProperty = null;
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private void OnPropertyValueChanged(object sender, Vector3 position)
        {
            SetPosition(position);
        }
    }
}