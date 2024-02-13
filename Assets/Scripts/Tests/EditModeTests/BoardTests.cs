using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.GameBoard.BoardPlayers;
using TicTacToe.Services.GameBoard.Rules;
using TicTacToe.Services.Hint;
using TicTacToe.Tests.Common;
using UnityEngine;

namespace TicTacToe.Tests.EditModeTests
{
    public class BoardTests : ZenjectUnitTestFixture
    {
        [OneTimeSetUp]
        public void InstallBindings()
        {
            Container.Bind<IGameRules>().To<TicTacToeRules>().AsTransient();
            Container.Bind<GameBoardService>().AsTransient();
        }

        [TestCase(new [] {1, 0, 0, 0, 0, 1, 1, 1, 0}, 3)]
        [TestCase(new [] {1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 1, 0, 0, 0}, 4)]
        public void WhenCheckingTie_AndBoardFull_ThenTieReturnsTrue(int[] tiles, int boardSize)
        {
            var board = Container.Resolve<GameBoardService>();
            var players = GetSubstitutePlayers();
            
            board.SetBoardSize(boardSize);
            board.SetPlayers(players);
            FillBoard(tiles, boardSize, board, players);

            var isTie = board.IsTie();
            Assert.IsTrue(isTie);
        }
        
        [TestCase(new [] {1, 1, 1, 0, 0, 0, 0, 0, 0}, 3)]
        [TestCase(new [] {1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 4)]
        public void WhenCheckingTie_AndBoardFullAndHasWinner_ThenTieReturnsFalse(int[] tiles, int boardSize)
        {
            var board = Container.Resolve<GameBoardService>();
            var players = GetSubstitutePlayers();
            
            board.SetBoardSize(boardSize);
            board.SetPlayers(players);
            FillBoard(tiles, boardSize, board, players);

            var isTie = board.IsTie();
            Assert.IsFalse(isTie);
        }

        [TestCase(3)]
        [TestCase(4)]
        public void WhenCheckingTie_AndBoardEmpty_ThenTieReturnsFalse(int boardSize)
        {
            var board = Container.Resolve<GameBoardService>();
            board.SetBoardSize(boardSize);
            
            var isTie = board.IsTie();
            
            Assert.IsFalse(isTie);
        }
        
        [TestCase(new [] {1, 0, 1, 0, 1}, 3)]
        [TestCase(new [] {1, 0, 1, 0, 1}, 4)]
        public void WhenCheckingTie_AndBoardPartiallyEmpty_ThenTieReturnsFalse(int[] tiles, int boardSize)
        {
            var board = Container.Resolve<GameBoardService>();
            var players = GetSubstitutePlayers();
            
            board.SetBoardSize(boardSize);
            board.SetPlayers(players);
            FillBoard(tiles, boardSize, board, players);

            var isTie = board.IsTie();
            Assert.IsFalse(isTie);
        }
        
        [TestCase(new [] {1, 1, 1}, 3)]
        [TestCase(new [] {1, 1, 1, 1}, 4)]
        public void WhenCheckingWin_AndHasWinner_ThenGetWinnerReturnsWinner(int[] tiles, int boardSize)
        {
            var board = Container.Resolve<GameBoardService>();
            var players = GetSubstitutePlayers();
            
            board.SetBoardSize(boardSize);
            board.SetPlayers(players);
            FillBoard(tiles, boardSize, board, players);

            var winner = board.GetWinner(out _);
            Assert.AreSame(winner.Name, "O");
        }
        
        [TestCase(new [] {1, 0, 1}, 3)]
        [TestCase(new [] {1, 0, 1, 0}, 4)]
        public void WhenCheckingWin_AndHasNoWinner_ThenGetWinnerReturnsNull(int[] tiles, int boardSize)
        {
            var board = Container.Resolve<GameBoardService>();
            var players = GetSubstitutePlayers();
            
            board.SetBoardSize(boardSize);
            board.SetPlayers(players);
            FillBoard(tiles, boardSize, board, players);

            var winner = board.GetWinner(out _);
            Assert.IsNull(winner);
        }

        [TestCase(3)]
        [TestCase(4)]
        public void WhenUsingUndo_AndBoardIsNotEmpty_ThenBoardRemovesTiles(int boardSize)
        {
            var board = Container.Resolve<GameBoardService>();
            var players = GetSubstitutePlayers();
            
            board.SetBoardSize(boardSize);
            board.SetPlayers(players);
            FillBoard(new []{ 0, 0, 1 }, boardSize, board, players);

            board.Undo();
            var filledTiles = board.Board.Cast<GameTile>().Count(tile => tile.IsOccupied);
            Assert.AreEqual(2, filledTiles);
        }
        
        
        [TestCase(3)]
        [TestCase(4)]
        public void WhenUsingHint_AndBoardIsEmpty_ThenHintHasValue(int boardSize)
        {
            var board = Container.Resolve<GameBoardService>();
            var players = GetSubstitutePlayers();
            
            board.SetBoardSize(boardSize);
            board.SetPlayers(players);
            FillBoard(new []{ 0, 0, 1 }, boardSize, board, players);

            board.Undo();
            var filledTiles = board.Board.Cast<GameTile>().Count(tile => tile.IsOccupied);
            Assert.AreEqual(2, filledTiles);
        }

        private static void FillBoard(IReadOnlyList<int> tiles, int boardSize, GameBoardService board, IReadOnlyList<IPlayer> players)
        {
            for (var i = 0; i < tiles.Count; i++)
            {
                var x = i / boardSize;
                var y = i % boardSize;

                board.AddMoveCommand(new Vector2Int(x, y), players[tiles[i]]);
            }
        }

        private static IPlayer[] GetSubstitutePlayers()
        {
            var playerX = Substitute.For<IPlayer>();
            playerX.Name = "X";
            
            var playerO = Substitute.For<IPlayer>();
            playerO.Name = "O";

            return new[] { playerX, playerO };
        }
    }
}