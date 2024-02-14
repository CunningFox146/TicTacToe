using System.Linq;
using NUnit.Framework;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.Rules;
using TicTacToe.Tests.Common.Infrastructure;
using TicTacToe.Tests.Common.Util;

namespace TicTacToe.Tests.EditModeTests
{
    [TestFixture]
    public class UndoTests : ZenjectUnitTestFixture
    {
        public override void SetupTestContainer()
        {
            base.SetupTestContainer();
            
            Container.Bind<IGameRules>().To<TicTacToeRules>().AsTransient();
            Container.Bind<GameBoardService>().AsTransient();
        }
        

        [TestCase(3)]
        [TestCase(4)]
        public void WhenUsingUndo_AndBoardIsNotEmpty_ThenBoardRemovesTiles(int boardSize)
        {
            var board = Container.CreateBoard(boardSize);
            board.FillBoardRandomly();

            var occupiedTiles = board.GetOccupiedTiles();
            board.Undo();
            
            Assert.AreEqual(occupiedTiles - 1, board.GetOccupiedTiles());
        }
        
        [TestCase(3)]
        [TestCase(4)]
        public void WhenUsingUndo_AndBoardIsEmpty_ThenBoardRemovesTiles(int boardSize)
        {
            var board = Container.CreateBoard(boardSize);
            
            var occupiedTiles = board.GetOccupiedTiles(); 
            board.Undo();
            
            Assert.Zero(occupiedTiles);
            Assert.Zero(board.GetOccupiedTiles());
        }
        
        [TestCase(3, 20)]
        [TestCase(4, 99)]
        public void WhenUsingUndoMultipleTimes_AndBoardTilesLessThenUndoes_ThenBoardRemovesAllTiles(int boardSize, int undoCount)
        {
            var board = Container.CreateBoard(boardSize);
            board.FillBoardRandomly();
                
            var occupiedTiles = board.GetOccupiedTiles();

            for (var i = 0; i < undoCount; i++) 
                board.Undo();
            
            Assert.NotZero(occupiedTiles);
            Assert.Zero(board.GetOccupiedTiles());
        }
    }
}