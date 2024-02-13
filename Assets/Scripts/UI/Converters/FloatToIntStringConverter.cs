using UnityEngine;
using UnityMvvmToolkit.Core.Converters.PropertyValueConverters;

namespace TicTacToe.UI.Converters
{
    public class FloatToIntStringConverter: PropertyValueConverter<float, string>
    {
        public override string Convert(float value)
            => Mathf.FloorToInt(value + 0.5f).ToString();

        public override float ConvertBack(string value) 
            => int.TryParse(value, out var num) ? num : default(float);
    }
}