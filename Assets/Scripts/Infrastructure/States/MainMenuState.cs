using Cysharp.Threading.Tasks;
using TicTacToe.UI.Factories;
using TicTacToe.UI.ViewStack;

namespace TicTacToe.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly IUserInterfaceFactory _userInterfaceFactory;
        private readonly IViewStackSystem _viewStackSystem;

        public MainMenuState(IUserInterfaceFactory userInterfaceFactory, IViewStackSystem viewStackSystem)
        {
            _userInterfaceFactory = userInterfaceFactory;
            _viewStackSystem = viewStackSystem;
        }
        
        public async UniTask Enter()
        {
            _viewStackSystem.PushView(await _userInterfaceFactory.CreateMainMenu());
        }
    }
}