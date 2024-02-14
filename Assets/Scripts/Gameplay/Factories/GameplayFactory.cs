using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.BoardLine;
using TicTacToe.Gameplay.BoardTile;
using TicTacToe.Gameplay.GameBoard;
using TicTacToe.Infrastructure.AssetManagement;

namespace TicTacToe.Gameplay.Factories
{
    public class GameplayFactory : IGameplayFactory
    {
        private readonly GameBoardController.Factory _backgroundFactory;
        private readonly BoardLineController.Factory _fieldLineFactory;
        private readonly BoardTileController.Factory _fieldTileFactory;

        public GameplayFactory(GameBoardController.Factory backgroundFactory, BoardLineController.Factory fieldLineFactory, BoardTileController.Factory fieldTileFactory)
        {
            _backgroundFactory = backgroundFactory;
            _fieldLineFactory = fieldLineFactory;
            _fieldTileFactory = fieldTileFactory;
        }
        
        public async UniTask<IGameBoardController> CreateGameBoardController() 
            => await _backgroundFactory.Create(BundleNames.GenericBundle, GameplayAssetNames.GameBoardController);
        
        public async UniTask<IBoardLineController> CreateBoardLineController() 
        
            => await _fieldLineFactory.Create(BundleNames.GenericBundle, GameplayAssetNames.BoardLineController);
        public async UniTask<IBoardTileController> CreateBoardTileController() 
            => await _fieldTileFactory.Create(BundleNames.GenericBundle, GameplayAssetNames.BoardTileController);
    }
}