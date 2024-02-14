using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.SceneManagement;
using TicTacToe.Infrastructure.States;
using TicTacToe.UI.Factories;
using TicTacToe.UI.ViewStack;

namespace TicTacToe.Gameplay.States
{
    public class GameEndState : IState
    {
        private readonly IUserInterfaceFactory _userInterfaceFactory;
        private readonly ISceneLoader _sceneLoader;
        private readonly IViewStackService _viewStack;

        public GameEndState(IUserInterfaceFactory userInterfaceFactory, IViewStackService viewStack)
        {
            _userInterfaceFactory = userInterfaceFactory;
            _viewStack = viewStack;
        }
        
        public async UniTask Enter()
        {
            _viewStack.PushView(await _userInterfaceFactory.CreateGameEndView());
        }
    }
}