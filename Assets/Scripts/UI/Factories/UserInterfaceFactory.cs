using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.UI.Views;

namespace TicTacToe.UI.Factories
{
    public class UserInterfaceFactory : IUserInterfaceFactory
    {
        private readonly LoadingCurtainView.Factory _loadingViewFactory;
        private readonly MainMenuView.Factory _mainMenuFactory;
        private readonly HUDView.Factory _hudViewFactory;

        public UserInterfaceFactory(LoadingCurtainView.Factory loadingViewFactory, MainMenuView.Factory mainMenuFactory, HUDView.Factory hudViewFactory)
        {
            _loadingViewFactory = loadingViewFactory;
            _mainMenuFactory = mainMenuFactory;
            _hudViewFactory = hudViewFactory;
        }

        public UniTask<MainMenuView> CreateMainMenuView() 
            => _mainMenuFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.MainView);
        
        public UniTask<LoadingCurtainView> CreateLoadingView()
            => _loadingViewFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.LoadingCurtainView);

        public UniTask<HUDView> CreateHUDView()
            => _hudViewFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.HUDView);
    }
}