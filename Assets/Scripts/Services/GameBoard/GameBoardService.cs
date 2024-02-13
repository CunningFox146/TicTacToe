using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Field;
using TicTacToe.Services.Commands;
using TicTacToe.Services.GameBoard.BoardPlayers;
using TicTacToe.Services.GameBoard.Rules;
using UnityEngine;

namespace TicTacToe.Services.GameBoard
{
    public class GameBoardService : IGameBoardService
    {
        public event Action<float> CountdownStarted; 
        private readonly IGameRules _gameRules;
        private readonly Stack<ICommand> _actions = new();
        private CancellationTokenSource _timeoutToken;

        public IGameField Field { get; set; }
        public GameTile[,] Board { get; private set; }
        public IPlayer CurrentPlayer { get; private set; }
        public List<IPlayer> Players { get; } = new();

        public GameBoardService(IGameRules gameRules)
        {
            _gameRules = gameRules;
        }

        public async UniTask PickTurn()
        {
            foreach (var player in Players)
            {
                Debug.Log($"{IsTie()} || {GetWinner(out _)}");
                if (IsTie() || GetWinner(out _) is not null)
                    break;
                
                CurrentPlayer = player;

                StartCountdown();
                var move = await CurrentPlayer.PickMove(_timeoutToken.Token);
                _timeoutToken.Dispose();
                
                if (move is null)
                {
                    // TODO: Player lost
                    return;
                }
                
                AddMoveCommand(move.Value);
            }

            CurrentPlayer = null;
        }

        private void AddMoveCommand(Vector2Int move)
        {
            void RunAction() => Board[move.x, move.y].SetPlayer(CurrentPlayer);
            void UndoAction() => Board[move.x, move.y].SetPlayer(null);
            var command = new Command(RunAction, UndoAction);
            _actions.Push(command);
        }

        private void StartCountdown()
        {
            var timeoutTime = 5;
            _timeoutToken?.Dispose();

            _timeoutToken = new CancellationTokenSource();
            _timeoutToken.CancelAfter(TimeSpan.FromSeconds(timeoutTime));
            
            CountdownStarted?.Invoke(timeoutTime);
        }

        public void Undo()
        {
            if (_actions.TryPop(out var command))
                command.Undo();
        }

        public IPlayer GetWinner(out int score) 
            => _gameRules.GetWinner(Board, out score);

        public bool IsTie()
            => _gameRules.IsTie(Board);
        
        public GameTile GetTile(int x, int y)
            => Board[x, y];

        public void SetBoardSize(int size)
        {
            Board = new GameTile[size, size];
            for (var x = 0; x < size; x++)
            for (var y = 0; y < size; y++)
                Board[x, y] = new GameTile(new Vector2Int(x, y));
        }

        public void SetPlayers(IEnumerable<IPlayer> players)
        {
            Players.AddRange(players);
        }
    }
}