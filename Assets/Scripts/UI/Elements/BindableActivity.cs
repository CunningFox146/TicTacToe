using UnityEngine;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace TicTacToe.UI.Elements
{
    public class BindableActivity : MonoBehaviour, IBindableElement
    {
        [SerializeField] private string _bindingVisibilityPath;
        
        private PropertyBindingData _propertyBindingData;
        private IProperty<bool> _visibilityProperty;

        public void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            _propertyBindingData ??= _bindingVisibilityPath.ToPropertyBindingData();

            _visibilityProperty = objectProvider.RentProperty<bool>(context, _propertyBindingData);
            _visibilityProperty.ValueChanged += OnPropertyValueChanged;

            UpdateVisibility(_visibilityProperty.Value);
        }

        public void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_visibilityProperty is null)
                return;

            _visibilityProperty.ValueChanged -= OnPropertyValueChanged;
            objectProvider.ReturnReadOnlyProperty(_visibilityProperty);
            _visibilityProperty = null;

            UpdateVisibility(default);
        }

        private void OnPropertyValueChanged(object sender, bool value)
        {
            UpdateVisibility(value);
        }

        private void UpdateVisibility(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}