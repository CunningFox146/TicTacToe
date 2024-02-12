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
            BindUserInterfaceFactory();
            BindViewStack();
        }
        
        private void BindViewFactories()
        {
            Container
                .BindFactory<string, string, UniTask<MainMenuView>, MainMenuView.Factory>()
                .FromFactory<PrefabFactoryAsync<MainMenuView>>();
            
            Container
                .BindFactory<string, string, UniTask<HUDView>, HUDView.Factory>()
                .FromFactory<PrefabFactoryAsync<HUDView>>();
        }
        
        private void BindViewModelFactory()
            => Container.Bind<ViewModelFactory>().AsSingle();


        private void BindUserInterfaceFactory() 
            => Container.Bind<IUserInterfaceFactory>().To<UserInterfaceFactory>().AsSingle();

        private void BindViewStack() 
            => Container.Bind<IViewStackService>().To<ViewStackService>().AsSingle();
    }
}