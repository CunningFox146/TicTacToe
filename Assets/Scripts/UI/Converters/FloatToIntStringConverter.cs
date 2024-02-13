using System.Globalization;
using UnityEngine;
using UnityMvvmToolkit.Core.Converters.PropertyValueConverters;

namespace TicTacToe.UI.Converters
{
    public class FloatToIntStringConverter: PropertyValueConverter<float, string>
    {
        public override string Convert(float value)
            => Mathf.Ceil(value).ToString(CultureInfo.InvariantCulture);

        public override float ConvertBack(string value) 
            => int.TryParse(value, out var num) ? num : default(float);
    }
}