using TicTacToe.Infrastructure.States;
using TicTacToe.UI;
using UnityEngine;
using Zenject;

namespace TicTacToe.Infrastructure
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