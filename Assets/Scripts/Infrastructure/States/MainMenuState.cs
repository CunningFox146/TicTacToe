using Cysharp.Threading.Tasks;
using TicTacToe.UI.Factories;
using TicTacToe.UI.Services.Loading;
using TicTacToe.UI.ViewStack;

namespace TicTacToe.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly ILoadingCurtainService _loadingCurtain;
        private readonly IUserInterfaceFactory _userInterfaceFactory;
        private readonly IViewStackService _viewStackService;

        public MainMenuState(IUserInterfaceFactory userInterfaceFactory, IViewStackService viewStackService,
            ILoadingCurtainService loadingCurtain)
        {
            _userInterfaceFactory = userInterfaceFactory;
            _viewStackService = viewStackService;
            _loadingCurtain = loadingCurtain;
        }

        public async UniTask Enter()
        {
            _viewStackService.PushView(await _userInterfaceFactory.CreateMainMenuView());
            _loadingCurtain.HideLoadingCurtain();
        }
    }
}