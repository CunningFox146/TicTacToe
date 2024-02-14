using System;
using System.Collections.Generic;
using System.Linq;
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
        private IPlayer _outOfTimePlayer;

        public IGameField Field { get; private set; }
        public GameTile[,] Board { get; private set; }
        public IPlayer CurrentPlayer { get; private set; }
        public List<IPlayer> Players { get; } = new();

        public GameBoardService(IGameRules gameRules)
        {
            _gameRules = gameRules;
        }

        public async UniTask PickMove()
        {
            foreach (var player in Players)
            {
                CurrentPlayer = player;

                StartCountdown();
                var move = await CurrentPlayer.PickMove(_timeoutToken.Token);
                _timeoutToken.Dispose();
                
                if (move is null)
                {
                    _outOfTimePlayer = player;
                    return;
                }
                
                AddMoveCommand(move.Value, CurrentPlayer);
                
                if (GetWinner(out _) is not null || IsTie())
                    break;
            }

            CurrentPlayer = null;
        }

        public void AddMoveCommand(Vector2Int move, IPlayer player)
        {
            void RunAction() => Board[move.x, move.y].SetPlayer(player);
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
        {
            score = 0;
            if (_outOfTimePlayer is not null)
                return Players.First(p => p != _outOfTimePlayer);
            return _gameRules.GetWinner(Board, out score);
        }

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

        public void SetField(IGameField field)
            => Field = field;

#if UNITY_INCLUDE_TESTS
        public void SetCurrentPlayer(IPlayer player)
            => CurrentPlayer = player;
#endif
    }
}