using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TicTacToe.Gameplay.Field;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.Infrastructure.Installers;
using TicTacToe.Infrastructure.SceneManagement;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.GameBoard.Rules;
using TicTacToe.Services.Hint;
using TicTacToe.Services.Randomizer;
using TicTacToe.Tests.Common;
using TicTacToe.Tests.Common.Infrastructure;
using TicTacToe.Tests.Common.Util;
using TicTacToe.UI;
using TicTacToe.UI.Factories;
using TicTacToe.UI.Services.Loading;
using TicTacToe.UI.Views;
using UnityEngine;
using UnityEngine.TestTools;

namespace TicTacToe.Tests.PlayModeTests
{
    public abstract class HudTestBase : ZenjectPlayModeUnitTestFixture
    {
        public override void SetupGlobalContainer()
        {
            base.SetupGlobalContainer();
            GlobalContainer.Bind<IAssetProvider>().To<AssetBundleProvider>().AsSingle();
            GlobalContainer.Bind<Camera>().FromComponentInNewPrefabResource(TestAssetNames.CameraResourcesPath).AsSingle();
        }
        
        public override void SetupTestContainer()
        {
            base.SetupTestContainer();
            
            Container.Bind<ISceneLoader>().FromInstance(Substitute.For<ISceneLoader>()).AsSingle();
            Container.Bind<ILoadingCurtainService>().FromInstance(Substitute.For<ILoadingCurtainService>()).AsSingle();
            Container.Bind<IGameRules>().To<TicTacToeRules>().AsSingle();
            Container.Bind<IRandomService>().To<RandomService>().AsSingle();
            Container.Bind<IHintService>().To<HintService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameBoardService>().AsSingle();
            
            GameplayFactoryInstaller.Install(Container);
            UserInterfaceInstaller.Install(Container);
        }

        [OneTimeSetUp]
        public void UnloadAssets()
        {
            var assetProvider = GlobalContainer.Resolve<IAssetProvider>();
            assetProvider.UnloadAssets();
        }
        
        [UnitySetUp]
        public IEnumerator LoadBundle() => UniTask.ToCoroutine(async () =>
        {
            var assetProvider = GlobalContainer.Resolve<IAssetProvider>();
            await assetProvider.LoadBundle(BundleNames.GenericBundle);
        });

        protected async UniTask<HUDView> CreateGameBoard(GameBoardService gameBoard, int fieldSize, IGameField field)
        {
            var factory = Container.Resolve<IUserInterfaceFactory>();
            var hud = await factory.CreateHUDView();

            gameBoard.SetPlayers(TestUtil.GetSubstitutePlayers());
            gameBoard.SetBoardSize(fieldSize);
            gameBoard.SetCurrentPlayer(gameBoard.Players.First());

            field.SetFieldSize(fieldSize);
            await field.Init();
            gameBoard.SetField(field);
            return hud;
        }
    }
}