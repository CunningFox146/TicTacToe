using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.UI.Converters;
using TicTacToe.UI.Factories;
using TicTacToe.UI.Views;
using TicTacToe.UI.ViewStack;
using UnityMvvmToolkit.Core.Interfaces;
using Zenject;

namespace TicTacToe.Infrastructure.Installers
{
    public class UserInterfaceInstaller : Installer<UserInterfaceInstaller>
    {
        public override void InstallBindings()
        {
            BindConverters();
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
                .BindFactory<string, string, UniTask<SkinPopupView>, SkinPopupView.Factory>()
                .FromFactory<PrefabFactoryAsync<SkinPopupView>>();
            
            Container
                .BindFactory<string, string, UniTask<HUDView>, HUDView.Factory>()
                .FromFactory<PrefabFactoryAsync<HUDView>>();
            
            Container
                .BindFactory<string, string, UniTask<GameEndView>, GameEndView.Factory>()
                .FromFactory<PrefabFactoryAsync<GameEndView>>();
        }
        
        
        private void BindConverters()
        {
            Container.Bind<IValueConverter[]>().FromInstance(new IValueConverter[]
            {
                new FloatToIntStringConverter(),
                new FloatToBool(),
            });
        }
        
        private void BindViewModelFactory()
            => Container.Bind<ViewModelFactory>().AsSingle();


        private void BindUserInterfaceFactory() 
            => Container.Bind<IUserInterfaceFactory>().To<UserInterfaceFactory>().AsSingle();

        private void BindViewStack() 
            => Container.Bind<IViewStackService>().To<ViewStackService>().AsSingle();
    }
}