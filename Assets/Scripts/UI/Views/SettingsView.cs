using Cysharp.Threading.Tasks;
using TicTacToe.UI.ViewModels;
using Zenject;

namespace TicTacToe.UI.Views
{
    public class SettingsView : ViewBase<SettingsViewModel>
    {
        public class Factory : PlaceholderFactory<string, string, UniTask<SettingsView>>
        {
        }
    }
}