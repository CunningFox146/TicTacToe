using NUnit.Framework;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.Rules;
using TicTacToe.Tests.Common.Infrastructure;

namespace TicTacToe.Tests.EditModeTests
{
    [TestFixture]
    public class WinTests : ZenjectUnitTestFixture
    {
        public override void SetupTestContainer()
        {
            base.SetupTestContainer();

            Container.Bind<IGameRules>().To<TicTacToeRules>().AsTransient();
            Container.Bind<GameBoardService>().AsTransient();
        }

        [TestCase(new[] { 1, 1, 1 }, 3)]
        [TestCase(new[] { 1, 1, 1, 1 }, 4)]
        public void WhenCheckingWin_AndHasWinner_ThenGetWinnerReturnsWinner(int[] tiles, int boardSize)
        {
            var board = Container.CreateFilledBoard(tiles, boardSize);
            var winner = board.GetWinner();
            Assert.AreSame(winner.Name, "O");
        }

        [TestCase(new[] { 1, 0, 1 }, 3)]
        [TestCase(new[] { 1, 0, 1, 0 }, 4)]
        public void WhenCheckingWin_AndHasNoWinner_ThenGetWinnerReturnsNull(int[] tiles, int boardSize)
        {
            var board = Container.CreateFilledBoard(tiles, boardSize);

            var winner = board.GetWinner();
            Assert.IsNull(winner);
        }
    }
}