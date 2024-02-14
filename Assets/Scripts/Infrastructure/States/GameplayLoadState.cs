using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.SceneManagement;
using TicTacToe.UI.Services.Loading;

namespace TicTacToe.Infrastructure.States
{
    public class GameplayLoadState : IState
    {
        private readonly ILoadingCurtainService _loadingCurtain;
        private readonly ISceneLoader _sceneLoader;

        public GameplayLoadState(ISceneLoader sceneLoader, ILoadingCurtainService loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public async UniTask Enter()
        {
            _loadingCurtain.ShowLoadingCurtain();
            await _sceneLoader.LoadScene(SceneIndex.Gameplay);
        }
    }
}