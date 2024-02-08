using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.SceneManagement;

namespace TicTacToe.Infrastructure.States
{
    public class GameBootstrapState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStateMachine _stateMachine;

        public GameBootstrapState(ISceneLoader sceneLoader, IStateMachine stateMachine)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
        }
        
        public async UniTask Enter()
        {
            await _sceneLoader.LoadScene(SceneIndex.MainMenu);
            await _stateMachine.Enter<MainMenuState>();
        }
    }
}