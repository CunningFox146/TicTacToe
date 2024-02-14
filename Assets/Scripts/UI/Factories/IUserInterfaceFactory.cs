using Cysharp.Threading.Tasks;
using TicTacToe.UI.Views;

namespace TicTacToe.UI.Factories
{
    public interface IUserInterfaceFactory
    {
        UniTask<MainMenuView> CreateMainMenuView();
        UniTask<HUDView> CreateHUDView();
        UniTask<GameEndView> CreateGameEndView();
        UniTask<SkinPopupView> CreateSkinPopupView();
        UniTask<SettingsView> CreateSettingsView();
        UniTask<GameModeSelectView> CreateGameModeSelectView();
    }
}