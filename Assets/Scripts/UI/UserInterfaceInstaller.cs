using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.UI.Factories;
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
            Container.Bind<MainMenuViewModel>().AsTransient();
            
            Container
                .BindFactory<string, string, UniTask<MainMenuView>, MainMenuView.Factory>()
                .FromFactory<PrefabFactoryAsync<MainMenuView>>();

            Container.BindInterfacesTo<ViewStackSystem>().AsSingle();
            Container.Bind<IUserInterfaceFactory>().To<UserInterfaceFactory>().AsSingle();
        }
    }
}