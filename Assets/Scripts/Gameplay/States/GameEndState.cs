using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.SceneManagement;
using TicTacToe.Infrastructure.States;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.Sounds;
using TicTacToe.UI.Factories;
using TicTacToe.UI.ViewStack;

namespace TicTacToe.Gameplay.States
{
    public class GameEndState : IState
    {
        private readonly IUserInterfaceFactory _userInterfaceFactory;
        private readonly ISceneLoader _sceneLoader;
        private readonly IViewStackService _viewStack;
        private readonly ISoundSource _soundSource;
        private readonly IGameBoardService _gameBoard;

        public GameEndState(IUserInterfaceFactory userInterfaceFactory, IViewStackService viewStack, ISoundSource soundSource, IGameBoardService gameBoard)
        {
            _userInterfaceFactory = userInterfaceFactory;
            _viewStack = viewStack;
            _soundSource = soundSource;
            _gameBoard = gameBoard;
        }
        
        public async UniTask Enter()
        {
            _soundSource.PlaySound(_gameBoard.IsTie() ? SoundNames.Tie : SoundNames.GameWIn).Forget();
            _viewStack.PushView(await _userInterfaceFactory.CreateGameEndView());
        }
    }
}