using UnityEngine;
using UnityEngine.UI;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace TicTacToe.UI.Elements
{
    public class BindableGraphicVisibility : MonoBehaviour, IBindableElement
    {
        [SerializeField] private Graphic _graphic;
        [SerializeField] private string _bindingVisibilityPath;
        
        private IProperty<bool> _visibilityProperty;
        private PropertyBindingData _propertyBindingData;
        
        public void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            _propertyBindingData ??= _bindingVisibilityPath.ToPropertyBindingData();

            _visibilityProperty = objectProvider.RentProperty<bool>(context, _propertyBindingData);
            _visibilityProperty.ValueChanged += OnPropertyValueChanged;

            UpdateVisibility(_visibilityProperty.Value);
        }

        private void OnPropertyValueChanged(object sender, bool value) 
            => UpdateVisibility(value);

        private void UpdateVisibility(bool value) 
            => _graphic.enabled = value;

        public void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_visibilityProperty is null)
                return;

            _visibilityProperty.ValueChanged -= OnPropertyValueChanged;
            objectProvider.ReturnReadOnlyProperty(_visibilityProperty);
            _visibilityProperty = null;

            UpdateVisibility(default);
        }
    }
}