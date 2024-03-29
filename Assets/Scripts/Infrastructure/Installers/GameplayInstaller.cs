using TicTacToe.Gameplay.Factories;
using TicTacToe.Infrastructure.States;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.Hint;
using TicTacToe.Services.Interactable;
using TicTacToe.Services.Rules;
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
            BindPlayerFactory();
            BindGameBoard();
            BindUserInterface();
        }
        
        private void BindGameBoard() 
            => Container.Bind<IGameBoardService>().To<GameBoardService>().AsSingle();

        private void BindPlayerFactory() 
            => Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();

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