using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.Infrastructure.States;
using TicTacToe.StaticData.Gameplay;
using TicTacToe.UI.Services.Loading;
using TicTacToe.UI.ViewStack;

namespace TicTacToe.Gameplay.States
{
    public class GameplayInitState : IState
    {
        private readonly IGameplayFactory _factory;
        private readonly IViewStackService _viewStack;
        private readonly ILoadingCurtainService _loadingCurtain;
        private readonly IAssetProvider _assetProvider;

        public GameplayInitState(IGameplayFactory factory, IViewStackService viewStack, ILoadingCurtainService loadingCurtain, IAssetProvider assetProvider)
        {
            _factory = factory;
            _viewStack = viewStack;
            _loadingCurtain = loadingCurtain;
            _assetProvider = assetProvider;
        }
        
        public async UniTask Enter()
        {
            _viewStack.ClearStack();
            
            var settings = await _assetProvider.LoadAsset<GameplaySettings>(GameplayAssetNames.GameplaySettings);
            var field = await _factory.CreateGameField();
            await field.Init();
            
            _loadingCurtain.HideLoadingCurtain();
        }
    }
}