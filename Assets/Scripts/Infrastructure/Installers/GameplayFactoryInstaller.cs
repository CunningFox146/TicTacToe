using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.BoardLine;
using TicTacToe.Gameplay.BoardTile;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Gameplay.GameBoard;
using TicTacToe.Infrastructure.AssetManagement;
using Zenject;

namespace TicTacToe.Infrastructure.Installers
{
    public class GameplayFactoryInstaller : Installer<GameplayFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<string, string, UniTask<GameBoardController>, GameBoardController.Factory>()
                .FromFactory<PrefabFactoryAsync<GameBoardController>>();
            
            Container
                .BindFactory<string, string, UniTask<BoardLineController>, BoardLineController.Factory>()
                .FromFactory<PrefabFactoryAsync<BoardLineController>>();
            
            Container
                .BindFactory<string, string, UniTask<BoardTileController>, BoardTileController.Factory>()
                .FromFactory<PrefabFactoryAsync<BoardTileController>>();
            
            Container.Bind<IGameplayFactory>().To<GameplayFactory>().AsSingle();
        }
    }
}