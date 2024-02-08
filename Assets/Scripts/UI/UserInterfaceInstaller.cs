using TicTacToe.UI.Factories;
using UnityEngine;
using Zenject;

namespace TicTacToe.UI
{
    public class UserInterfaceInstaller : MonoInstaller
    {
        [SerializeField] private UserInterfaceAssets _uiAssets;
        
        public override void InstallBindings()
        {
            Container.Bind<UserInterfaceAssets>().FromInstance(_uiAssets).AsSingle();
            Container.Bind<IUserInterfaceFactory>().To<UserInterfaceFactory>().AsSingle();
        }
    }
}