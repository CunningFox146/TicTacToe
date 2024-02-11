using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TicTacToe.Services.GameBoard.BoardPlayers;

namespace TicTacToe.Services.GameBoard
{
    public interface IGameBoardService
    {
        int BoardSize { get; }
        UniTask PickTurn();
        GameTile GetTile(int x, int y);
        bool IsTie();
        IPlayer GetWinner();
        void SetBoardSize(int size);
        void SetPlayers(IEnumerable<IPlayer> players);
    }
}