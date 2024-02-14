using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.GameBoard;
using TicTacToe.Services.BoardPlayers;
using TicTacToe.Services.Commands;
using TicTacToe.Services.Rules;
using UnityEngine;

namespace TicTacToe.Services.GameBoard
{
    public class GameBoardService : IGameBoardService
    {
        private readonly Stack<ICommand> _actions = new();
        private readonly IGameRules _gameRules;
        private IPlayer _outOfTimePlayer;
        private CancellationTokenSource _timeoutTokenSource;
        private int _timeoutTime = 5;

        public GameBoardService(IGameRules gameRules)
        {
            _gameRules = gameRules;
        }

        public event Action<float, CancellationToken> CountdownStarted;

        public IGameBoardController BoardController { get; private set; }
        public GameTile[,] Board { get; private set; }
        public IPlayer CurrentPlayer { get; private set; }
        public List<IPlayer> Players { get; } = new();

        public async UniTask PickMove()
        {
            foreach (var player in Players)
            {
                CurrentPlayer = player;
                
                StartCountdown();
                var move = await CurrentPlayer.PickMove(_timeoutTokenSource.Token);
                _timeoutTokenSource.Cancel();

                if (move is null)
                {
                    _outOfTimePlayer = player;
                    return;
                }

                AddMoveCommand(CurrentPlayer, move.Value);

                if (GetWinner(out _) is not null || IsTie())
                    break;
            }

            CurrentPlayer = null;
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
            return _gameRules.GetWinner(Board);
        }

        public bool IsTie() 
            => _gameRules.IsTie(Board);

        public void SetBoardSize(int size)
        {
            Board = new GameTile[size, size];
            for (var x = 0; x < size; x++)
            for (var y = 0; y < size; y++)
                Board[x, y] = new GameTile(new Vector2Int(x, y));
        }

        public void SetPlayers(IEnumerable<IPlayer> players) 
            => Players.AddRange(players);

        public void SetField(IGameBoardController boardController) 
            => BoardController = boardController;

        public void AddMoveCommand(IPlayer player, Vector2Int move)
        {
            void RunAction() 
                => Board[move.x, move.y].SetPlayer(player);

            void UndoAction() 
                => Board[move.x, move.y].SetPlayer(null);

            var command = new Command(RunAction, UndoAction);
            _actions.Push(command);
        }

        private void StartCountdown()
        {
            _timeoutTokenSource?.Dispose();

            _timeoutTokenSource = new CancellationTokenSource();
            _timeoutTokenSource.CancelAfter(TimeSpan.FromSeconds(_timeoutTime));

            CountdownStarted?.Invoke(_timeoutTime, _timeoutTokenSource.Token);
        }

#if UNITY_INCLUDE_TESTS
        public void SetCurrentPlayer(IPlayer player)
        {
            CurrentPlayer = player;
        }
#endif
    }
}