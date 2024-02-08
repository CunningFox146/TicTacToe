using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.UI.Views;

namespace TicTacToe.UI.Factories
{
    public class UserInterfaceFactory : IUserInterfaceFactory
    {
        private readonly LoadingCurtainView.Factory _loadingViewFactory;
        private readonly MainMenuView.Factory _mainMenuFactory;

        public UserInterfaceFactory(LoadingCurtainView.Factory loadingViewFactory, MainMenuView.Factory mainMenuFactory)
        {
            _loadingViewFactory = loadingViewFactory;
            _mainMenuFactory = mainMenuFactory;
        }

        public UniTask<MainMenuView> CreateMainMenuView() 
            => _mainMenuFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.MainView);
        public UniTask<LoadingCurtainView> CreateLoadingView()
            => _loadingViewFactory.Create(BundleNames.GenericBundle, UserInterfaceAssetNames.LoadingCurtainView);
    }
}