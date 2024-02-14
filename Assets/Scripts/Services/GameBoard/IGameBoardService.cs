using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.GameBoard;
using TicTacToe.Services.GameBoard.BoardPlayers;

namespace TicTacToe.Services.GameBoard
{
    public interface IGameBoardService : IUndoable
    {
        event Action<float> CountdownStarted; 
        
        IGameBoardController BoardController { get;}
        GameTile[,] Board { get; }
        IPlayer CurrentPlayer { get; }
        List<IPlayer> Players { get; }
        UniTask PickMove();
        GameTile GetTile(int x, int y);
        bool IsTie();
        IPlayer GetWinner(out int score);
        void SetBoardSize(int size);
        void SetPlayers(IEnumerable<IPlayer> players);
        void SetField(IGameBoardController boardController);
    }
}