using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.Infrastructure.States;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.GameBoard.Rules;
using TicTacToe.Services.Hint;
using TicTacToe.Services.Interactable;
using TicTacToe.UI;
using UnityEngine;
using Zenject;

namespace TicTacToe.Infrastructure.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMainCamera();
            BindInteractionService();
            BindHintService();
            BindStateFactory();
            BindGameplayFactory();
            BindRules();
            BindGameBoard();
            BindUserInterface();
        }
        
        private void BindGameBoard()
        {
            Container.Bind<BoardControllerFactory>().AsSingle();
            Container.Bind<IGameBoardService>().To<GameBoardService>().AsSingle();
        }

        private void BindRules()
            => Container.Bind<IGameRules>().To<TicTacToeRules>().AsTransient();

        private void BindMainCamera()
            => Container.Bind<Camera>().FromInstance(Camera.main).AsSingle();
        
        private void BindHintService() 
            => Container.Bind<IHintService>().To<HintService>().AsSingle();
        
        private void BindInteractionService()
            => Container.BindInterfacesTo<InteractionService>().AsSingle().NonLazy();

        private void BindUserInterface()
            => UserInterfaceInstaller.Install(Container);

        private void BindStateFactory() 
            => Container.Bind<StateFactory>().AsSingle();

        private void BindGameplayFactory()
            => GameplayFactoryInstaller.Install(Container);
    }
}