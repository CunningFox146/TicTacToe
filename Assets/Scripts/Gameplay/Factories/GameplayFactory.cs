using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Field;
using TicTacToe.Infrastructure.AssetManagement;

namespace TicTacToe.Gameplay.Factories
{
    public class GameplayFactory : IGameplayFactory
    {
        private readonly GameField.Factory _backgroundFactory;

        public GameplayFactory(GameField.Factory backgroundFactory)
        {
            _backgroundFactory = backgroundFactory;
        }
        
        public async UniTask<IGameField> CreateBackground() 
            => await _backgroundFactory.Create(BundleNames.GenericBundle, GameplayAssetNames.GameField);
    }
}