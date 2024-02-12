using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TicTacToe.Services.GameBoard.BoardPlayers;

namespace TicTacToe.Services.GameBoard
{
    public interface IGameBoardService
    {
        GameTile[,] Board { get; }
        IPlayer CurrentPlayer { get; }
        List<IPlayer> Players { get; }
        UniTask PickTurn();
        GameTile GetTile(int x, int y);
        bool IsTie();
        IPlayer GetWinner(out int score);
        void SetBoardSize(int size);
        void SetPlayers(IEnumerable<IPlayer> players);
    }
}