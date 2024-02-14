using System.Collections.Generic;
using System.ComponentModel;
using TicTacToe.Services.GameBoard;
using TicTacToe.Tests.Common.Util;
using Zenject;

namespace TicTacToe.Tests.Common.Infrastructure
{
    public static class DiContainerExtensions
    {
        public static GameBoardService CreateFilledBoard(this DiContainer container, IReadOnlyList<int> tiles, int boardSize)
        {
            var board = container.CreateBoard(boardSize);
            board.FillBoard(tiles);
            return board;
        }

        public static GameBoardService CreateBoard(this DiContainer container, int boardSize)
        {
            var board = container.Resolve<GameBoardService>();
            var players = TestUtil.GetSubstitutePlayers();
            
            board.SetBoardSize(boardSize);
            board.SetPlayers(players);
            
            return board;
        }
    }
}