using UnityMvvmToolkit.Core.Converters.ParameterValueConverters;

namespace TicTacToe.UI.Converters
{
    public class StringToFloatConverter :  ParameterValueConverter<float>
    {
        public override float Convert(string value) 
            => float.TryParse(value, out var num) ? num : default;
    }
}