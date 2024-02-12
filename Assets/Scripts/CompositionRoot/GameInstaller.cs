using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.Infrastructure.States;
using TicTacToe.Infrastructure.SceneManagement;
using TicTacToe.Services.Input;
using TicTacToe.Services.Log;
using TicTacToe.Services.Randomizer;
using TicTacToe.Services.Skin;
using TicTacToe.StaticData.Gameplay;
using TicTacToe.UI;
using TicTacToe.UI.Services.Loading;
using TicTacToe.UI.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TicTacToe.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private EventSystem _eventSystem;

        public override void InstallBindings()
        {
            BindEventSystem();
            BindLogService();
            BindSceneLoader();
            BindRandomService();
            BindInputService();
            BindAssetProvider();
            BindSkinService();
            BindLoadingCurtain();
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<StateFactory>().AsSingle();
            Container.Bind<IStateMachine>().To<GameStateMachine>().AsSingle();
        }

        private void BindInputService()
        {
            Container.Bind<GameplayInput>().AsTransient();
            Container.BindInterfacesTo<InputService>().AsSingle();
        }

        private void BindLoadingCurtain()
        {
            Container
                .BindFactory<string, string, UniTask<LoadingCurtainView>, LoadingCurtainView.Factory>()
                .FromFactory<PrefabFactoryAsync<LoadingCurtainView>>();

            Container.Bind<ILoadingCurtainService>().To<LoadingCurtainService>().AsSingle();
        }

        private void BindSkinService() 
            => Container.BindInterfacesTo<SkinService>().AsSingle();
        
        private void BindEventSystem()
            => Container.Bind<EventSystem>().FromInstance(_eventSystem).AsSingle();

        private void BindAssetProvider() 
            => Container.Bind<IAssetProvider>().To<AssetBundleProvider>().AsSingle();
        
        private void BindRandomService() 
            => Container.Bind<IRandomService>().To<RandomService>().AsSingle();
        
        private void BindSceneLoader()
            => Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();

        private void BindLogService()
            => Container.Bind<ILogService>().To<UnityLogService>().AsSingle();
    }
}
