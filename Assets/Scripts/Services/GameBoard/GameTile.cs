using System;
using TicTacToe.Services.GameBoard.Controllers;

namespace TicTacToe.Services.GameBoard
{
    public class GameTile
    {
        public event Action StateChanged;
        
        private IBoardController _player;
        public bool IsOccupied => _player is not null;

        public void SetPlayer(IBoardController player)
        {
            _player = player;
            StateChanged?.Invoke();
        }
    }
}