using System.Collections;
using System.Linq;
using Cysharp.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.Infrastructure.Installers;
using TicTacToe.Infrastructure.SceneManagement;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.GameBoard.Rules;
using TicTacToe.Services.Hint;
using TicTacToe.Services.Randomizer;
using TicTacToe.Tests.Common;
using TicTacToe.UI;
using TicTacToe.UI.Elements;
using TicTacToe.UI.Factories;
using TicTacToe.UI.Services.Loading;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace TicTacToe.Tests.PlayModeTests
{
    public class HintTests : ZenjectUnitTestFixture
    {
        [OneTimeSetUp]
        public void InstallBindings()
        {
            Container.Bind<Camera>().FromComponentInNewPrefabResource(TestAssetNames.CameraResourcesPath).AsSingle();
            Container.Bind<IAssetProvider>().To<AssetBundleProvider>().AsSingle();
            Container.Bind<ISceneLoader>().FromInstance(Substitute.For<ISceneLoader>()).AsSingle();
            Container.Bind<ILoadingCurtainService>().FromInstance(Substitute.For<ILoadingCurtainService>()).AsSingle();
            Container.Bind<IGameRules>().To<TicTacToeRules>().AsSingle();
            Container.Bind<IRandomService>().To<RandomService>().AsSingle();
            Container.Bind<IHintService>().To<HintService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameBoardService>().AsSingle();
            
            GameplayFactoryInstaller.Install(Container);
            UserInterfaceInstaller.Install(Container);
        }


        [UnitySetUp]
        public IEnumerator LoadBundle() => UniTask.ToCoroutine(async () =>
        {
            var assetProvider = Container.Resolve<IAssetProvider>();
            await assetProvider.LoadBundle(BundleNames.GenericBundle);
        });

        [UnityTest]
        [Timeout(2000)]
        public IEnumerator UITest() => UniTask.ToCoroutine(async () =>
        {
            var gameFactory = Container.Resolve<IGameplayFactory>();
            var gameBoard = Container.Resolve<GameBoardService>();
            var factory = Container.Resolve<IUserInterfaceFactory>();
            var hud = await factory.CreateHUDView().AsTask();

            gameBoard.SetPlayers(TestUtil.GetSubstitutePlayers());
            gameBoard.SetBoardSize(3);
            gameBoard.SetCurrentPlayer(gameBoard.Players.First());

            var field = await gameFactory.CreateGameField();
            field.SetFieldSize(3);
            await field.Init();
            gameBoard.SetField(field);

            var buttons = hud.GetComponentsInChildren<Button>();
            var hintButton = buttons.First(b => b.name == TestAssetNames.HudHintButtonName);
            hintButton.onClick?.Invoke();
            var pointer = hud.GetComponentsInChildren<BindableActivity>(true)
                .First(e => e.name == TestAssetNames.HudHintPointerName);

            await UniTask.WaitUntil(() => pointer.gameObject.activeSelf);

            Assert.IsTrue(pointer.gameObject.activeSelf);
            Object.Destroy(hud.gameObject);
        });
    }
}