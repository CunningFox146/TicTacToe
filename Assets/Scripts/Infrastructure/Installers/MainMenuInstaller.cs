using TicTacToe.Infrastructure.States;
using TicTacToe.UI;
using Zenject;

namespace TicTacToe.Infrastructure.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameStateFactory();
            BindUserInterface();
        }

        private void BindGameStateFactory()
            => Container.Bind<StateFactory>().AsSingle();

        private void BindUserInterface()
            => UserInterfaceInstaller.Install(Container);
    }
}