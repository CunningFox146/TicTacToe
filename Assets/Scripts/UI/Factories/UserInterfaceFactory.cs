using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.UI.Views;

namespace TicTacToe.UI.Factories
{
    public class UserInterfaceFactory : IUserInterfaceFactory
    {
        private readonly MainMenuView.Factory _mainMenuFactory;

        public UserInterfaceFactory(MainMenuView.Factory mainMenuFactory)
        {
            _mainMenuFactory = mainMenuFactory;
        }

        public UniTask<MainMenuView> CreateMainMenu() => _mainMenuFactory.Create(BundleNames.GenericBundleName, UserInterfaceAssetNames.MainView);

    }
}