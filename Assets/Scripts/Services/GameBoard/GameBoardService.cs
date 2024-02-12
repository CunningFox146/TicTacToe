using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Field;
using TicTacToe.Services.Commands;
using TicTacToe.Services.GameBoard.BoardPlayers;
using TicTacToe.Services.GameBoard.Rules;

namespace TicTacToe.Services.GameBoard
{
    public class GameBoardService : IGameBoardService
    {
        private readonly IGameRules _gameRules;
        private readonly Stack<ICommand> _actions = new();

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
                CurrentPlayer = player;
                
                using var token = new CancellationTokenSource();
                token.CancelAfter(TimeSpan.FromSeconds(5));

                var turn = await CurrentPlayer.PickTurn(token.Token);
                if (turn is null)
                {
                    // TODO: Player lost
                    return;
                }
                
                void RunAction() => Board[turn.Value.x, turn.Value.y].SetPlayer(player);
                void UndoAction() => Board[turn.Value.x, turn.Value.y].SetPlayer(null);
                var command = new Command(RunAction, UndoAction);
                _actions.Push(command);
                
                if (IsTie() || GetWinner(out _) is not null)
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
                Board[x, y] = new GameTile(x, y);
        }

        public void SetPlayers(IEnumerable<IPlayer> players)
        {
            Players.AddRange(players);
        }
    }
}