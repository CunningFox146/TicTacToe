using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Field;
using TicTacToe.Gameplay.Line;
using TicTacToe.Gameplay.Tile;
using TicTacToe.Infrastructure.AssetManagement;

namespace TicTacToe.Gameplay.Factories
{
    public class GameplayFactory : IGameplayFactory
    {
        private readonly GameField.Factory _backgroundFactory;
        private readonly FieldLine.Factory _fieldLineFactory;
        private readonly FieldTile.Factory _fieldTileFactory;

        public GameplayFactory(GameField.Factory backgroundFactory, FieldLine.Factory fieldLineFactory, FieldTile.Factory fieldTileFactory)
        {
            _backgroundFactory = backgroundFactory;
            _fieldLineFactory = fieldLineFactory;
            _fieldTileFactory = fieldTileFactory;
        }
        
        public async UniTask<IGameField> CreateGameField() 
            => await _backgroundFactory.Create(BundleNames.GenericBundle, GameplayAssetNames.GameField);
        
        public async UniTask<IFieldLine> CreateFieldLine() 
        
            => await _fieldLineFactory.Create(BundleNames.GenericBundle, GameplayAssetNames.Line);
        public async UniTask<IFieldTile> CreateFieldTile() 
            => await _fieldTileFactory.Create(BundleNames.GenericBundle, GameplayAssetNames.Tile);
    }
}