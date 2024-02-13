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

        public UserInterfaceFactory(LoadingCurtainView.Factory loadingFactory, MainMenuView.Factory mainMenuFactory, HUDView.Factory hudFactory, GameEndView.Factory gameEndFactory)
        {
            _loadingFactory = loadingFactory;
            _mainMenuFactory = mainMenuFactory;
            _hudFactory = hudFactory;
            _gameEndFactory = gameEndFactory;
        }

        public UniTask<MainMenuView> CreateMainMenuView() 
            => _mainMenuFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.MainView);

        public UniTask<HUDView> CreateHUDView()
            => _hudFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.HUDView);
        
        public UniTask<GameEndView> CreateGameEndView()
            => _gameEndFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.GameEndView);
    }
}