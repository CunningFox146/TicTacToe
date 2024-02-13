using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.UI.Views;

namespace TicTacToe.UI.Factories
{
    public class UserInterfaceFactory : IUserInterfaceFactory
    {
        private readonly LoadingCurtainView.Factory _loadingFactory;
        private readonly MainMenuView.Factory _mainMenuFactory;
        private readonly HUDView.Factory _hudFactory;
        private readonly GameEndView.Factory _gameEndFactory;
        private readonly SkinPopupView.Factory _skinPopupFactory;

        public UserInterfaceFactory(LoadingCurtainView.Factory loadingFactory, MainMenuView.Factory mainMenuFactory, HUDView.Factory hudFactory, GameEndView.Factory gameEndFactory, SkinPopupView.Factory skinPopupFactory)
        {
            _loadingFactory = loadingFactory;
            _mainMenuFactory = mainMenuFactory;
            _hudFactory = hudFactory;
            _gameEndFactory = gameEndFactory;
            _skinPopupFactory = skinPopupFactory;
        }

        public UniTask<MainMenuView> CreateMainMenuView() 
            => _mainMenuFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.MainView);

        public UniTask<HUDView> CreateHUDView()
            => _hudFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.HUDView);
        
        public UniTask<GameEndView> CreateGameEndView()
            => _gameEndFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.GameEndView);
        
        public UniTask<SkinPopupView> CreateSkinPopupView()
            => _skinPopupFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.SkinPopupView);
    }
}