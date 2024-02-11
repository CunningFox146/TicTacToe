using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Gameplay.Field;
using TicTacToe.Gameplay.Line;
using TicTacToe.Gameplay.Tile;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.Infrastructure.States;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.Interactable;
using TicTacToe.UI.Factories;
using UnityEngine;
using Zenject;

namespace TicTacToe.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMainCamera();
            BindInteractionService();
            BindStateFactory();
            BindViewModelFactory();
            BindGameplayFactory();
            BindGameBoard();
        }

        private void BindGameBoard()
        {
            Container.Bind<BoardControllerFactory>().AsSingle();
            Container.Bind<IGameBoardService>().To<GameBoardService>().AsSingle();
        }

        private void BindMainCamera()
            => Container.Bind<Camera>().FromInstance(Camera.main).AsSingle();

        private void BindInteractionService()
            => Container.BindInterfacesTo<InteractionService>().AsSingle().NonLazy();

        private void BindViewModelFactory() 
            => Container.Bind<ViewModelFactory>().AsSingle();

        private void BindStateFactory() 
            => Container.Bind<StateFactory>().AsSingle();

        private void BindGameplayFactory()
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