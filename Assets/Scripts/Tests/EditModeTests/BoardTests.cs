using System.Linq;
using NUnit.Framework;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.GameBoard.Rules;
using TicTacToe.Tests.Common;

namespace TicTacToe.Tests.EditModeTests
{
    [TestFixture]
    public class BoardTests : ZenjectUnitTestFixture
    {
        [OneTimeSetUp]
        public void InstallBindings()
        {
            GlobalContainer.Bind<IGameRules>().To<TicTacToeRules>().AsTransient();
            GlobalContainer.Bind<GameBoardService>().AsTransient();
        }

        [TestCase(new [] {1, 0, 0, 0, 0, 1, 1, 1, 0}, 3)]
        [TestCase(new [] {1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 1, 0, 0, 0}, 4)]
        public void WhenCheckingTie_AndBoardFull_ThenTieReturnsTrue(int[] tiles, int boardSize)
        {
            var board = GlobalContainer.Resolve<GameBoardService>();
            var players = TestUtil.GetSubstitutePlayers();
            
            board.SetBoardSize(boardSize);
            board.SetPlayers(players);
            board.FillBoard(tiles);

            var isTie = board.IsTie();
            Assert.IsTrue(isTie);
        }
        
        [TestCase(new [] {1, 1, 1, 0, 0, 0, 0, 0, 0}, 3)]
        [TestCase(new [] {1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 4)]
        public void WhenCheckingTie_AndBoardFullAndHasWinner_ThenTieReturnsFalse(int[] tiles, int boardSize)
        {
            var board = GlobalContainer.Resolve<GameBoardService>();
            var players = TestUtil.GetSubstitutePlayers();
            
            board.SetBoardSize(boardSize);
            board.SetPlayers(players);
            board.FillBoard(tiles);

            var isTie = board.IsTie();
            Assert.IsFalse(isTie);
        }

        [TestCase(3)]
        [TestCase(4)]
        public void WhenCheckingTie_AndBoardEmpty_ThenTieReturnsFalse(int boardSize)
        {
            var board = GlobalContainer.Resolve<GameBoardService>();
            board.SetBoardSize(boardSize);
            
            var isTie = board.IsTie();
            
            Assert.IsFalse(isTie);
        }
        
        [TestCase(new [] {1, 0, 1, 0, 1}, 3)]
        [TestCase(new [] {1, 0, 1, 0, 1}, 4)]
        public void WhenCheckingTie_AndBoardPartiallyEmpty_ThenTieReturnsFalse(int[] tiles, int boardSize)
        {
            var board = GlobalContainer.Resolve<GameBoardService>();
            var players = TestUtil.GetSubstitutePlayers();
            
            board.SetBoardSize(boardSize);
            board.SetPlayers(players);
            board.FillBoard(tiles);

            var isTie = board.IsTie();
            Assert.IsFalse(isTie);
        }
        
        [TestCase(new [] {1, 1, 1}, 3)]
        [TestCase(new [] {1, 1, 1, 1}, 4)]
        public void WhenCheckingWin_AndHasWinner_ThenGetWinnerReturnsWinner(int[] tiles, int boardSize)
        {
            var board = GlobalContainer.Resolve<GameBoardService>();
            var players = TestUtil.GetSubstitutePlayers();
            
            board.SetBoardSize(boardSize);
            board.SetPlayers(players);
            board.FillBoard(tiles);

            var winner = board.GetWinner(out _);
            Assert.AreSame(winner.Name, "O");
        }
        
        [TestCase(new [] {1, 0, 1}, 3)]
        [TestCase(new [] {1, 0, 1, 0}, 4)]
        public void WhenCheckingWin_AndHasNoWinner_ThenGetWinnerReturnsNull(int[] tiles, int boardSize)
        {
            var board = GlobalContainer.Resolve<GameBoardService>();
            var players = TestUtil.GetSubstitutePlayers();
            
            board.SetBoardSize(boardSize);
            board.SetPlayers(players);
            board.FillBoard(tiles);

            var winner = board.GetWinner(out _);
            Assert.IsNull(winner);
        }

        [TestCase(3)]
        [TestCase(4)]
        public void WhenUsingUndo_AndBoardIsNotEmpty_ThenBoardRemovesTiles(int boardSize)
        {
            var board = GlobalContainer.Resolve<GameBoardService>();
            var players = TestUtil.GetSubstitutePlayers();
            
            board.SetBoardSize(boardSize);
            board.SetPlayers(players);
            board.FillBoard(new []{ 0, 0, 1 });

            board.Undo();
            var filledTiles = board.Board.Cast<GameTile>().Count(tile => tile.IsOccupied);
            Assert.AreEqual(2, filledTiles);
        }
        
        
        [TestCase(3)]
        [TestCase(4)]
        public void WhenUsingHint_AndBoardIsEmpty_ThenHintHasValue(int boardSize)
        {
            var board = GlobalContainer.Resolve<GameBoardService>();
            var players = TestUtil.GetSubstitutePlayers();
            
            board.SetBoardSize(boardSize);
            board.SetPlayers(players);
            board.FillBoard(new []{ 0, 0, 1 });

            board.Undo();
            var filledTiles = board.Board.Cast<GameTile>().Count(tile => tile.IsOccupied);
            Assert.AreEqual(2, filledTiles);
        }
    }
}