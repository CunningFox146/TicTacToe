using System;
using TicTacToe.Services.GameBoard.BoardPlayers;

namespace TicTacToe.Services.GameBoard
{
    public class GameTile
    {
        public event Action StateChanged;
        
        public IPlayer Player { get; private set; }
        public bool IsOccupied => Player is not null;

        public void SetPlayer(IPlayer player)
        {
            Player = player;
            StateChanged?.Invoke();
        }
    }
}