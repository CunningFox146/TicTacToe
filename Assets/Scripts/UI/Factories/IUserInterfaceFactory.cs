using Cysharp.Threading.Tasks;
using TicTacToe.UI.Views;

namespace TicTacToe.UI.Factories
{
    public interface IUserInterfaceFactory
    {
        UniTask<MainMenuView> CreateMainMenuView();
        UniTask<LoadingCurtainView> CreateLoadingView();
    }
}