using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Field;
using TicTacToe.Gameplay.Line;
using TicTacToe.Infrastructure.AssetManagement;

namespace TicTacToe.Gameplay.Factories
{
    public class GameplayFactory : IGameplayFactory
    {
        private readonly GameField.Factory _backgroundFactory;
        private readonly FieldLine.Factory _fieldLineFactory;

        public GameplayFactory(GameField.Factory backgroundFactory, FieldLine.Factory fieldLineFactory)
        {
            _backgroundFactory = backgroundFactory;
            _fieldLineFactory = fieldLineFactory;
        }
        
        public async UniTask<IGameField> CreateGameField() 
            => await _backgroundFactory.Create(BundleNames.GenericBundle, GameplayAssetNames.GameField);
        
        public async UniTask<IFieldLine> CreateFieldLine() 
            => await _fieldLineFactory.Create(BundleNames.GenericBundle, GameplayAssetNames.Line);
    }
}