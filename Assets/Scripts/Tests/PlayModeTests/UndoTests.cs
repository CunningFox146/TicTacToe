using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Services.GameBoard;
using TicTacToe.Tests.Common;
using TicTacToe.Tests.Common.Util;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace TicTacToe.Tests.PlayModeTests
{
    public class UndoTests : HudTestBase
    {
        [UnityTest]
        public IEnumerator WhenPressingUndoButton_And3x3FieldIsFilled_ThenTilesShouldBeCleared()
            => UniTask.ToCoroutine(async () => { await PressUndoWhenFieldIsFilled(3); });
        
        [UnityTest]
        public IEnumerator WhenPressingUndoButton_And4x4FieldIsFilled_ThenTilesShouldBeCleared()
            => UniTask.ToCoroutine(async () => { await PressUndoWhenFieldIsFilled(4); });
        
        [UnityTest]
        public IEnumerator WhenPressingUndoButton_And3x3FieldIsEmpty_ThenTilesShouldBeCleared()
            => UniTask.ToCoroutine(async () => { await PressUndoWhenFieldIsEmpty(3); });
        
        [UnityTest]
        public IEnumerator WhenPressingUndoButton_And4x4FieldIsEmpty_ThenTilesShouldBeCleared()
            => UniTask.ToCoroutine(async () => { await PressUndoWhenFieldIsEmpty(4); });

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
        }
        
        private async UniTask PressUndoWhenFieldIsEmpty(int fieldSize)
        {
            var gameFactory = Container.Resolve<IGameplayFactory>();
            var gameBoard = Container.Resolve<GameBoardService>();
            var field = await gameFactory.CreateGameField();
            var hud = await CreateGameBoard(gameBoard, fieldSize, field);
            var undoButton = hud.GetObjectByName<Button>(TestAssetNames.HudUndoButtonName);
            
            var occupied = field.GetOccupiedTiles(fieldSize);
            undoButton.onClick?.Invoke();
            
            Assert.Zero(occupied);
            Assert.Zero(field.GetOccupiedTiles(fieldSize));
        }
    }
}