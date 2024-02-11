using System;
using TicTacToe.Services.GameBoard.BoardPlayers;
using UnityEngine;

namespace TicTacToe.Services.GameBoard
{
    public class GameTile
    {
        public event Action StateChanged;
        
        public IPlayer Player { get; private set; }
        public Vector2Int Position { get; private set; }
        public bool IsOccupied => Player is not null;

        public GameTile(int x, int y)
        {
            Position = new Vector2Int(x, y);
        }

        public void SetPlayer(IPlayer player)
        {
            Player = player;
            StateChanged?.Invoke();
        }
    }
}