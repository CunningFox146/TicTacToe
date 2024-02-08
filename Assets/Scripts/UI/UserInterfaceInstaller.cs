using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.UI.Factories;
using TicTacToe.UI.Services.Loading;
using TicTacToe.UI.ViewModels;
using TicTacToe.UI.Views;
using TicTacToe.UI.ViewStack;
using Zenject;

namespace TicTacToe.UI
{
    public class UserInterfaceInstaller : Installer<UserInterfaceInstaller>
    {
        public override void InstallBindings()
        {
            BindViewModels();
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
        
        private void BindViewModels()
        {
            Container.Bind<MainMenuViewModel>().AsTransient();
        }

        private void BindLoadingService() 
            => Container.Bind<ILoadingCurtainService>().To<LoadingCurtainService>().AsSingle();

        private void BindUserInterfaceFactory() 
            => Container.Bind<IUserInterfaceFactory>().To<UserInterfaceFactory>().AsSingle();

        private void BindViewStack() 
            => Container.BindInterfacesTo<ViewStackSystem>().AsSingle();
        

    }
}