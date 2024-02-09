using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Gameplay.GameplayBg;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.Infrastructure.States;
using TicTacToe.UI.Views;
using UnityEngine;
using Zenject;

namespace TicTacToe.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<string, string, UniTask<GameplayBackground>, GameplayBackground.Factory>()
                .FromFactory<PrefabFactoryAsync<GameplayBackground>>();
            
            Container.Bind<StateFactory>().AsSingle();
            BindGameplayFactory();
            BindCamera();
        }

        private void BindCamera() 
            => Container.Bind<Camera>().FromInstance(Camera.main);

        private void BindGameplayFactory() 
            => Container.Bind<IGameplayFactory>().To<GameplayFactory>().AsSingle();
    }
}