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

        public UserInterfaceFactory(MainMenuView.Factory mainMenuFactory, SettingsView.Factory settingsFactory, HUDView.Factory hudFactory,
            GameEndView.Factory gameEndFactory, SkinPopupView.Factory skinPopupFactory)
        {
            _mainMenuFactory = mainMenuFactory;
            _settingsFactory = settingsFactory;
            _hudFactory = hudFactory;
            _gameEndFactory = gameEndFactory;
            _skinPopupFactory = skinPopupFactory;
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
    }
}