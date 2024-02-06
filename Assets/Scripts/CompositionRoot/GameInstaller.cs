using TicTacToe.Infrastructure.States;
using TicTacToe.Infrastructure.SceneManagement;
using TicTacToe.Services.Log;
using TicTacToe.Services.Randomizer;
using Zenject;

namespace TicTacToe.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallLogService();
            InstallSceneLoader();
            InstallRandomService();
            InstallGameStateMachine();
        }

        private void InstallGameStateMachine()
        {
            Container.Bind<StateFactory>().AsSingle();
            Container.Bind<IStateMachine>().To<GameStateMachine>().AsSingle();
        }
        
        private void InstallRandomService() 
            => Container.Bind<IRandomService>().To<RandomService>().AsSingle();
        
        private void InstallSceneLoader()
            => Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();

        private void InstallLogService()
            => Container.Bind<ILogService>().To<UnityLogService>().AsSingle();
    }
}
