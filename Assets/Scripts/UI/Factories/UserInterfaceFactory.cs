using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.UI.Views;

namespace TicTacToe.UI.Factories
{
    public class UserInterfaceFactory : IUserInterfaceFactory
    {
        private readonly GameEndView.Factory _gameEndFactory;
        private readonly SettingsView.Factory _settingsFactory;
        private readonly HUDView.Factory _hudFactory;
        private readonly LoadingCurtainView.Factory _loadingFactory;
        private readonly MainMenuView.Factory _mainMenuFactory;
        private readonly SkinPopupView.Factory _skinPopupFactory;
        private readonly GameModeSelectView.Factory _gameModeSelectFactory;

        public UserInterfaceFactory(MainMenuView.Factory mainMenuFactory, SettingsView.Factory settingsFactory, HUDView.Factory hudFactory,
            GameEndView.Factory gameEndFactory, SkinPopupView.Factory skinPopupFactory, GameModeSelectView.Factory gameModeSelectFactory)
        {
            _mainMenuFactory = mainMenuFactory;
            _settingsFactory = settingsFactory;
            _hudFactory = hudFactory;
            _gameEndFactory = gameEndFactory;
            _skinPopupFactory = skinPopupFactory;
            _gameModeSelectFactory = gameModeSelectFactory;
        }

        public UniTask<MainMenuView> CreateMainMenuView() 
            => _mainMenuFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.MainView);

        public UniTask<HUDView> CreateHUDView() 
            => _hudFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.HUDView);

        public UniTask<GameEndView> CreateGameEndView()
        {
            return _gameEndFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.GameEndView);
        }

        public UniTask<SkinPopupView> CreateSkinPopupView() 
            => _skinPopupFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.SkinPopupView);
        
        public UniTask<SettingsView> CreateSettingsView() 
            => _settingsFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.SettingsView);
        
        public UniTask<GameModeSelectView> CreateGameModeSelectView()
            => _gameModeSelectFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.GameModeSelectView);
    }
}