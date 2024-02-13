using UnityEngine;
using UnityMvvmToolkit.Core.Converters.PropertyValueConverters;

namespace TicTacToe.UI.Converters
{
    public class FloatToBool : PropertyValueConverter<float, bool>
    {
        public override bool Convert(float value)
            => !Mathf.Approximately(value, 0f);

        public override float ConvertBack(bool value)
            => value ? 1f : 0f;
    }
}