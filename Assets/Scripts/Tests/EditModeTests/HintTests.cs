using NSubstitute;
using NUnit.Framework;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.GameBoard.BoardPlayers;
using TicTacToe.Services.GameBoard.Rules;
using TicTacToe.Services.Hint;
using TicTacToe.Services.Randomizer;
using TicTacToe.Tests.Common.Infrastructure;
using TicTacToe.Tests.Common.Util;
using UnityEngine;

namespace TicTacToe.Tests.EditModeTests
{
    [TestFixture]
    public class HintTests : ZenjectUnitTestFixture
    {
        public override void SetupTestContainer()
        {
            base.SetupTestContainer();
            
            Container.Bind<IGameRules>().To<TicTacToeRules>().AsTransient();
            Container.Bind<IRandomService>().To<RandomService>().AsTransient();
            Container.Bind<HintService>().AsTransient();
        }

        [TestCase(3)]
        [TestCase(4)]
        public void WhenUsingHint_AndBoardIsEmpty_ThenHintHasValue(int boardSize)
        {
            var hintService = Container.Resolve<HintService>();
            var board = TestUtil.GetMockBoard(boardSize);
            var playerX = Substitute.For<IPlayer>();
            var playerO = Substitute.For<IPlayer>();
            var move = hintService.GetBestMoveSync(board, playerX, playerO);
            Assert.IsNotNull(move);
        }
        
        [TestCase(3)]
        [TestCase(4)]
        public void WhenUsingHint_AndBoardIsNotEmpty_ThenHintReturnsValue(int boardSize)
        {
            var hintService = Container.Resolve<HintService>();
            var board = TestUtil.GetMockBoard(boardSize);
            var playerX = Substitute.For<IPlayer>();
            var playerO = Substitute.For<IPlayer>();
            board[0, 0].SetPlayer(playerX);
            board[1, 1].SetPlayer(playerO);
            board[2, 2].SetPlayer(playerX);

            var move = hintService.GetBestMoveSync(board, playerX, playerO);
            Assert.IsNotNull(move);
        }
        
        [TestCase(3)]
        [TestCase(4)]
        public void WhenUsingHint_AndBoardHasOneTile_ThenHintReturnsThatTile(int boardSize)
        {
            var hintService = Container.Resolve<HintService>();
            var board = TestUtil.GetMockBoard(boardSize);
            var playerX = Substitute.For<IPlayer>();
            var playerO = Substitute.For<IPlayer>();

            for (var x = 0; x < boardSize; x++)
            for (var y = 0; y < boardSize; y++)
                board[x, y].SetPlayer(playerO);
            
            board[boardSize - 1, boardSize - 1].SetPlayer(null);

            var move = hintService.GetBestMoveSync(board, playerX, playerO);
            
            Assert.IsNotNull(move);
            Assert.AreEqual(move.Value, new Vector2Int(boardSize - 1, boardSize - 1));
        }
        
        [TestCase(3)]
        [TestCase(4)]
        public void WhenUsingHint_AndBoardIsFull_ThenHintReturnsNull(int boardSize)
        {
            var hintService = Container.Resolve<HintService>();
            var board = TestUtil.GetMockBoard(boardSize);
            var playerX = Substitute.For<IPlayer>();
            var playerO = Substitute.For<IPlayer>();

            for (var x = 0; x < boardSize; x++)
            for (var y = 0; y < boardSize; y++)
                board[x, y].SetPlayer(playerO);
            
            var move = hintService.GetBestMoveSync(board, playerX, playerO);
            
            Assert.IsNull(move);
        }
    }
}