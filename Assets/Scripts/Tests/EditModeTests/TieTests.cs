using NUnit.Framework;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.GameBoard.Rules;
using TicTacToe.Tests.Common.Infrastructure;

namespace TicTacToe.Tests.EditModeTests
{
    [TestFixture]
    public class TieTests : ZenjectUnitTestFixture
    {
        public override void SetupTestContainer()
        {
            base.SetupTestContainer();
            
            Container.Bind<IGameRules>().To<TicTacToeRules>().AsTransient();
            Container.Bind<GameBoardService>().AsTransient();
        }

        [TestCase(new [] {1, 0, 0, 0, 0, 1, 1, 1, 0}, 3)]
        [TestCase(new [] {1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 1, 0, 0, 0}, 4)]
        public void WhenCheckingTie_AndBoardFull_ThenTieReturnsTrue(int[] tiles, int boardSize)
        {
            var board = Container.CreateFilledBoard(tiles, boardSize);
            Assert.IsTrue(board.IsTie());
        }

        [TestCase(new [] {1, 1, 1, 0, 0, 0, 0, 0, 0}, 3)]
        [TestCase(new [] {1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 4)]
        public void WhenCheckingTie_AndBoardFullAndHasWinner_ThenTieReturnsFalse(int[] tiles, int boardSize)
        {
            var board = Container.CreateFilledBoard(tiles, boardSize);
            Assert.IsFalse(board.IsTie());
        }

        [TestCase(3)]
        [TestCase(4)]
        public void WhenCheckingTie_AndBoardEmpty_ThenTieReturnsFalse(int boardSize)
        {
            var board = Container.CreateBoard(boardSize);
            Assert.IsFalse(board.IsTie());
        }

        [TestCase(new [] {1, 0, 1, 0, 1}, 3)]
        [TestCase(new [] {1, 0, 1, 0, 1}, 4)]
        public void WhenCheckingTie_AndBoardPartiallyEmpty_ThenTieReturnsFalse(int[] tiles, int boardSize)
        {
            var board = Container.CreateFilledBoard(tiles, boardSize);
            Assert.IsFalse(board.IsTie());
        }
    }
}