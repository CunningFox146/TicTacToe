using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.GameplayBg;
using TicTacToe.Infrastructure.AssetManagement;

namespace TicTacToe.Gameplay.Factories
{
    public class GameplayFactory : IGameplayFactory
    {
        private readonly GameplayBackground.Factory _backgroundFactory;

        public GameplayFactory(GameplayBackground.Factory backgroundFactory)
        {
            _backgroundFactory = backgroundFactory;
        }
        
        public async UniTask<IGameplayBackground> CreateBackground() 
            => await _backgroundFactory.Create(BundleNames.GenericBundle, GameplayAssetNames.GameplayBg);
    }
}