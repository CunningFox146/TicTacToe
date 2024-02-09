using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.SceneManagement;
using TicTacToe.UI.Services.Loading;

namespace TicTacToe.Infrastructure.States
{
    public class GameplayLoadState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtainService _loadingCurtain;

        public GameplayLoadState(ISceneLoader sceneLoader, ILoadingCurtainService loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }
        
        public async UniTask Enter()
        {
            _loadingCurtain.ShowLoadingCurtain();
            // TODO: Load asset bundle
            await _sceneLoader.LoadScene(SceneIndex.Gameplay);
        }
    }
}