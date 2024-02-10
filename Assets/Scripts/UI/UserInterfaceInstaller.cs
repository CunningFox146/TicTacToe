using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.UI.Factories;
using TicTacToe.UI.Services.Loading;
using TicTacToe.UI.Views;
using TicTacToe.UI.ViewStack;
using Zenject;

namespace TicTacToe.UI
{
    public class UserInterfaceInstaller : Installer<UserInterfaceInstaller>
    {
        public override void InstallBindings()
        {
            BindViewModelFactory();
            BindViewFactories();
            BindLoadingService();
            BindUserInterfaceFactory();
            BindViewStack();
        }
        
        private void BindViewFactories()
        {
            Container
                .BindFactory<string, string, UniTask<LoadingCurtainView>, LoadingCurtainView.Factory>()
                .FromFactory<PrefabFactoryAsync<LoadingCurtainView>>();

            Container
                .BindFactory<string, string, UniTask<MainMenuView>, MainMenuView.Factory>()
                .FromFactory<PrefabFactoryAsync<MainMenuView>>();
        }
        
        private void BindViewModelFactory()
            => Container.Bind<ViewModelFactory>().AsSingle();

        private void BindLoadingService() 
            => Container.Bind<ILoadingCurtainService>().To<LoadingCurtainService>().AsSingle();

        private void BindUserInterfaceFactory() 
            => Container.Bind<IUserInterfaceFactory>().To<UserInterfaceFactory>().AsSingle();

        private void BindViewStack() 
            => Container.BindInterfacesTo<ViewStackService>().AsSingle();
    }
}