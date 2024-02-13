using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.SceneManagement;
using TicTacToe.Infrastructure.States;
using TicTacToe.UI.Factories;

namespace TicTacToe.Gameplay.States
{
    public class GameWonState : IState
    {
        private readonly IUserInterfaceFactory _userInterfaceFactory;
        private readonly ISceneLoader _sceneLoader;

        public GameWonState(IUserInterfaceFactory userInterfaceFactory)
        {
            _userInterfaceFactory = userInterfaceFactory;
        }
        
        public async UniTask Enter()
        {
            await _userInterfaceFactory.CreateGameEndView();
        }
    }
}