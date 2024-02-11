using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TicTacToe.Services.GameBoard.BoardPlayers;
using UnityEngine;

namespace TicTacToe.Services.GameBoard
{
    public class GameBoardService : IGameBoardService
    {
        private GameTile[,] _board;
        private readonly List<IPlayer> _players = new();
        
        public int BoardSize { get; private set; }

        public async UniTask PickTurn()
        {
            foreach (var player in _players)
            {
                using var token = new CancellationTokenSource();
                token.CancelAfter(TimeSpan.FromSeconds(5));
                
                var turn = await player.PickTurn(token.Token);
                _board[turn.x, turn.y].SetPlayer(player);
                
                if (IsTie() || GetWinner() is not null)
                    break;
            }
        }

        public IPlayer GetWinner()
        {
            Debug.Log("Check winner");
            return null;
        }
        
        public bool IsTie()
        {
            var tilesCount = 0;
            foreach (var tile in _board)
            {
                if (tile.IsOccupied)
                    tilesCount++;
            }

            return tilesCount == BoardSize * BoardSize;
        }
        
        public GameTile GetTile(int x, int y)
            => _board[x, y];

        public void SetBoardSize(int size)
        {
            BoardSize = size;
            _board = new GameTile[size, size];
            for (var x = 0; x < size; x++)
            for (var y = 0; y < size; y++)
                _board[x, y] = new GameTile();
        }

        public void SetPlayers(IEnumerable<IPlayer> players)
        {
            _players.AddRange(players);
        }
    }
}