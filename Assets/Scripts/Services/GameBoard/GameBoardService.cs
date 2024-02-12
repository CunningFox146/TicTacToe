using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TicTacToe.Services.Commands;
using TicTacToe.Services.GameBoard.BoardPlayers;
using UnityEngine;

namespace TicTacToe.Services.GameBoard
{
    public class GameBoardService : IGameBoardService
    {
        private readonly List<IPlayer> _players = new();
        private readonly Stack<ICommand> _actions = new();
        private GameTile[,] _board;
        
        public int BoardSize { get; private set; }
        public IPlayer CurrentPlayer { get; private set; }

        public async UniTask PickTurn()
        {
            foreach (var player in _players)
            {
                CurrentPlayer = player;
                
                using var token = new CancellationTokenSource();
                token.CancelAfter(TimeSpan.FromSeconds(5));

                var turn = await CurrentPlayer.PickTurn(token.Token);
                if (turn is null)
                {
                    // TODO: Player lost
                    return;
                }
                
                void RunAction() => _board[turn.Value.x, turn.Value.y].SetPlayer(player);
                void UndoAction() => _board[turn.Value.x, turn.Value.y].SetPlayer(null);
                var command = new Command(RunAction, UndoAction);
                _actions.Push(command);
                
                if (IsTie() || GetWinner() is not null)
                    break;
            }

            CurrentPlayer = null;
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
                _board[x, y] = new GameTile(x, y);
        }

        public void SetPlayers(IEnumerable<IPlayer> players)
        {
            _players.AddRange(players);
        }
    }
}