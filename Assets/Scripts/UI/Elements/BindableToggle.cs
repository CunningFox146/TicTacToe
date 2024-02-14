using UnityEngine;
using UnityEngine.UI;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace TicTacToe.UI.Elements
{
    [RequireComponent(typeof(Toggle))]
    public class BindableToggle : MonoBehaviour, IBindableElement
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private string _bindingPath;

        private PropertyBindingData _propertyBindingData;
        private IProperty<bool> _toggledProperty;

        public void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            _propertyBindingData ??= _bindingPath.ToPropertyBindingData();

            _toggledProperty = objectProvider.RentProperty<bool>(context, _propertyBindingData);
            _toggledProperty.ValueChanged += OnPropertyValueChanged;
            _toggle.onValueChanged.AddListener(OnToggleValueChange);

            SetToggled(_toggledProperty.Value);
        }

        public void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_toggledProperty is null)
                return;

            _toggledProperty.ValueChanged -= OnPropertyValueChanged;
            objectProvider.ReturnReadOnlyProperty(_toggledProperty);
            _toggledProperty = null;
            _toggle.onValueChanged.RemoveListener(OnToggleValueChange);
        }

        private void OnToggleValueChange(bool isEnabled)
        {
            _toggledProperty.Value = isEnabled;
        }

        private void OnPropertyValueChanged(object sender, bool isToggled) 
            => SetToggled(isToggled);

        private void SetToggled(bool b) 
            => _toggle.isOn = b;
    }
}