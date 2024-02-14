using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Gameplay.Field;
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
using TicTacToe.UI.Views;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace TicTacToe.Tests.PlayModeTests
{
    public class BoardTests : ZenjectUnitTestFixture
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
        public IEnumerator WhenPressingHintButton_And3x3FieldIsEmpty_ThenHintShouldAppear()
            => UniTask.ToCoroutine(async () => { await PressHintOnEmptyField(3); });
        
        [UnityTest]
        [Timeout(2000)]
        public IEnumerator WhenPressingHintButton_And4x4FieldIsEmpty_ThenHintShouldAppear()
            => UniTask.ToCoroutine(async () => { await PressHintOnEmptyField(4); });
        
        
        [UnityTest]
        public IEnumerator WhenPressingHintButton_And3x3FieldIsFilled_ThenHintShouldNotAppear()
            => UniTask.ToCoroutine(async () => { await PressHintWhenNoHintAvailable(3, 1); });
        
        [UnityTest]
        public IEnumerator WhenPressingHintButton_And4x4FieldIsFilled_ThenHintShouldNotAppear()
            => UniTask.ToCoroutine(async () => { await PressHintWhenNoHintAvailable(4, 1); });
        
        
        [UnityTest]
        public IEnumerator WhenPressingUndoButton_And3x3FieldIsFilled_ThenTilesShouldBeCleared()
            => UniTask.ToCoroutine(async () => { await PressUndoWhenFieldIsFilled(3); });
        
        [UnityTest]
        public IEnumerator WhenPressingUndoButton_And4x4FieldIsFilled_ThenTilesShouldBeCleared()
            => UniTask.ToCoroutine(async () => { await PressUndoWhenFieldIsFilled(4); });

        private async UniTask PressUndoWhenFieldIsFilled(int fieldSize)
        {
            var gameFactory = Container.Resolve<IGameplayFactory>();
            var gameBoard = Container.Resolve<GameBoardService>();
            var field = await gameFactory.CreateGameField();
            var hud = await CreateGameBoard(gameBoard, fieldSize, field);
            var undoButton = hud.GetObjectByName<Button>(TestAssetNames.HudUndoButtonName);
            
            gameBoard.FillBoardRandomly();
            var occupied = field.GetOccupiedTiles(fieldSize);
            undoButton.onClick?.Invoke();
            
            Assert.AreEqual(occupied - 2, field.GetOccupiedTiles(fieldSize));
            Object.Destroy(hud.gameObject);
        }

        private async UniTask PressHintWhenNoHintAvailable(int fieldSize, float timeout)
        {
            var gameFactory = Container.Resolve<IGameplayFactory>();
            var gameBoard = Container.Resolve<GameBoardService>();
            var field = await gameFactory.CreateGameField();
            var hud = await CreateGameBoard(gameBoard, fieldSize, field);
            var hintButton = hud.GetObjectByName<Button>(TestAssetNames.HudHintButtonName);
            var pointer = hud.GetObjectByName<BindableActivity>(TestAssetNames.HudHintPointerName);
            gameBoard.FillBoardRandomly();

            hintButton.onClick?.Invoke();

            await UniTask.Delay(TimeSpan.FromSeconds(timeout));

            Assert.IsFalse(pointer.gameObject.activeSelf);
            Object.Destroy(hud.gameObject);
        }

        private async UniTask PressHintOnEmptyField(int fieldSize)
        {
            var gameFactory = Container.Resolve<IGameplayFactory>();
            var gameBoard = Container.Resolve<GameBoardService>();
            var field = await gameFactory.CreateGameField();
            var hud = await CreateGameBoard(gameBoard, fieldSize, field);
            var hintButton = hud.GetObjectByName<Button>(TestAssetNames.HudHintButtonName);
            var pointer = hud.GetObjectByName<BindableActivity>(TestAssetNames.HudHintPointerName);

            hintButton.onClick?.Invoke();
            await UniTask.WaitUntil(() => pointer.gameObject.activeSelf);

            Assert.IsTrue(pointer.gameObject.activeSelf);
            Object.Destroy(hud.gameObject);
        }

        private async UniTask<HUDView> CreateGameBoard(GameBoardService gameBoard, int fieldSize, IGameField field)
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