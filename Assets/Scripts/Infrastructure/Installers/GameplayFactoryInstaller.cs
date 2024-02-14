using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Gameplay.Field;
using TicTacToe.Gameplay.Line;
using TicTacToe.Gameplay.Tile;
using TicTacToe.Infrastructure.AssetManagement;
using Zenject;

namespace TicTacToe.Infrastructure.Installers
{
    public class GameplayFactoryInstaller : Installer<GameplayFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<string, string, UniTask<GameField>, GameField.Factory>()
                .FromFactory<PrefabFactoryAsync<GameField>>();
            
            Container
                .BindFactory<string, string, UniTask<FieldLine>, FieldLine.Factory>()
                .FromFactory<PrefabFactoryAsync<FieldLine>>();
            
            Container
                .BindFactory<string, string, UniTask<FieldTile>, FieldTile.Factory>()
                .FromFactory<PrefabFactoryAsync<FieldTile>>();
            
            Container.Bind<IGameplayFactory>().To<GameplayFactory>().AsSingle();
        }
    }
}