using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.Infrastructure.SceneManagement;
using TicTacToe.UI.Services.Loading;

namespace TicTacToe.Infrastructure.States
{
    public class GameBootstrapState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IAssetProvider _assetProvider;
        private readonly IStateMachine _stateMachine;
        private readonly ILoadingCurtainService _loadingCurtain;

        public GameBootstrapState(ISceneLoader sceneLoader, IAssetProvider assetProvider, IStateMachine stateMachine, ILoadingCurtainService loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
            _stateMachine = stateMachine;
            _loadingCurtain = loadingCurtain;
        }
        
        public async UniTask Enter()
        {
            _assetProvider.UnloadAssets();
            await _assetProvider.LoadBundle(BundleNames.GenericBundle);
            _loadingCurtain.ShowLoadingCurtain();
            await _sceneLoader.LoadScene(SceneIndex.MainMenu);
            await _stateMachine.Enter<MainMenuState>();
        }
    }
}