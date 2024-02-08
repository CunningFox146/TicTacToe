using Cysharp.Threading.Tasks;
using TicTacToe.UI.Factories;
using TicTacToe.UI.Services.Loading;
using TicTacToe.UI.ViewStack;

namespace TicTacToe.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly IUserInterfaceFactory _userInterfaceFactory;
        private readonly IViewStackSystem _viewStackSystem;
        private readonly ILoadingCurtainService _loadingCurtain;

        public MainMenuState(IUserInterfaceFactory userInterfaceFactory, IViewStackSystem viewStackSystem, ILoadingCurtainService loadingCurtain)
        {
            _userInterfaceFactory = userInterfaceFactory;
            _viewStackSystem = viewStackSystem;
            _loadingCurtain = loadingCurtain;
        }
        
        public async UniTask Enter()
        {
            _viewStackSystem.PushView(await _userInterfaceFactory.CreateMainMenuView());
            _loadingCurtain.HideLoadingCurtain();
        }
    }
}