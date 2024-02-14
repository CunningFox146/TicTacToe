using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.Infrastructure.SceneManagement;
using TicTacToe.Services.Skin;
using TicTacToe.Services.Sounds;
using TicTacToe.UI.Services.Loading;

namespace TicTacToe.Infrastructure.States
{
    public class GameBootstrapState : IState
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ILoadingCurtainService _loadingCurtain;
        private readonly ISceneLoader _sceneLoader;
        private readonly ISkinService _skinService;
        private readonly ISoundSource _soundSource;

        public GameBootstrapState(ISceneLoader sceneLoader, IAssetProvider assetProvider,
            ILoadingCurtainService loadingCurtain, ISkinService skinService, ISoundSource soundSource)
        {
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
            _loadingCurtain = loadingCurtain;
            _skinService = skinService;
            _soundSource = soundSource;
        }

        public async UniTask Enter()
        {
            _assetProvider.UnloadAssets();
            await _assetProvider.LoadBundle(BundleNames.GenericBundle);
            _loadingCurtain.ShowLoadingCurtain();
            
            await _soundSource.Init();
            await _skinService.SetSkin(SkinItemNames.DefaultSkin);
            await _sceneLoader.LoadScene(SceneIndex.MainMenu);
        }
    }
}