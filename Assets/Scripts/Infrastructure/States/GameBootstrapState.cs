using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.Infrastructure.SceneManagement;
using TicTacToe.Services.Skin;
using TicTacToe.UI.Services.Loading;

namespace TicTacToe.Infrastructure.States
{
    public class GameBootstrapState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IAssetProvider _assetProvider;
        private readonly IStateMachine _stateMachine;
        private readonly ILoadingCurtainService _loadingCurtain;
        private readonly ISkinService _skinService;

        public GameBootstrapState(ISceneLoader sceneLoader, IAssetProvider assetProvider, IStateMachine stateMachine, ILoadingCurtainService loadingCurtain, ISkinService skinService)
        {
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
            _stateMachine = stateMachine;
            _loadingCurtain = loadingCurtain;
            _skinService = skinService;
        }
        
        public async UniTask Enter()
        {
            _assetProvider.UnloadAssets();
            await _assetProvider.LoadBundle(BundleNames.GenericBundle);
            _loadingCurtain.ShowLoadingCurtain();
            await _skinService.SetSkin(SkinItemNames.DefaultSkin);
            await _sceneLoader.LoadScene(SceneIndex.MainMenu);
        }
    }
}