using TicTacToe.UI.Factories;
using Zenject;

namespace TicTacToe.Infrastructure.Factories
{
    public class GameFactoryInstaller : Installer<GameFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        }
    }
}