using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Services.GameBoard;
using TicTacToe.Tests.Common;
using TicTacToe.Tests.Common.Util;
using TicTacToe.UI.Elements;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace TicTacToe.Tests.PlayModeTests
{
    [TestFixture]
    public class HintTests : HudTestBase
    {
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

        private async UniTask PressHintWhenNoHintAvailable(int fieldSize, float timeout)
        {
            var gameFactory = Container.Resolve<IGameplayFactory>();
            var gameBoard = Container.Resolve<GameBoardService>();
            var field = await gameFactory.CreateGameBoardController();
            var hud = await CreateGameBoard(gameBoard, fieldSize, field);
            var hintButton = hud.GetObjectByName<Button>(TestAssetNames.HudHintButtonName);
            var pointer = hud.GetObjectByName<BindableActivity>(TestAssetNames.HudHintPointerName);
            gameBoard.FillBoardRandomly();

            hintButton.onClick?.Invoke();

            await UniTask.Delay(TimeSpan.FromSeconds(timeout));

            Assert.IsFalse(pointer.gameObject.activeSelf);
        }

        private async UniTask PressHintOnEmptyField(int fieldSize)
        {
            var gameFactory = Container.Resolve<IGameplayFactory>();
            var gameBoard = Container.Resolve<GameBoardService>();
            var field = await gameFactory.CreateGameBoardController();
            var hud = await CreateGameBoard(gameBoard, fieldSize, field);
            var hintButton = hud.GetObjectByName<Button>(TestAssetNames.HudHintButtonName);
            var pointer = hud.GetObjectByName<BindableActivity>(TestAssetNames.HudHintPointerName);

            hintButton.onClick?.Invoke();
            await UniTask.WaitUntil(() => pointer.gameObject.activeSelf);

            Assert.IsTrue(pointer.gameObject.activeSelf);
        }
    }
}