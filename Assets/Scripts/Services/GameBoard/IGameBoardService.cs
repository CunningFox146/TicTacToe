using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.GameBoard;
using TicTacToe.Services.BoardPlayers;

namespace TicTacToe.Services.GameBoard
{
    public interface IGameBoardService : IUndoable, ICountdownSource
    {
        IGameBoardController BoardController { get; }
        GameTile[,] Board { get; }
        List<IPlayer> Players { get; }
        IPlayer CurrentPlayer { get; }

        void SetBoardSize(int size);
        void SetPlayers(IEnumerable<IPlayer> players);
        void SetField(IGameBoardController boardController);
        UniTask PickMove(CancellationToken cancellationToken);

        bool IsTie();
        IPlayer GetWinner();
    }
}