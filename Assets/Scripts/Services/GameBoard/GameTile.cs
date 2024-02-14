using System;
using TicTacToe.Services.BoardPlayers;
using UnityEngine;

namespace TicTacToe.Services.GameBoard
{
    public class GameTile : ICloneable
    {
        public event Action StateChanged;
        
        public IPlayer Player { get; private set; }
        public Vector2Int Position { get; private set; }
        public bool IsOccupied => Player is not null;

        public GameTile(Vector2Int position)
        {
            Position = position;
        }

        public void SetPlayer(IPlayer player)
        {
            Player = player;
            StateChanged?.Invoke();
        }

        public object Clone()
        {
            var tile = new GameTile(Position);
            tile.SetPlayer(Player);
            return tile;
        }
    }
}