using TicTacToe.Services.GameBoard;
using TicTacToe.Services.GameBoard.BoardPlayers;
using TicTacToe.Services.GameBoard.Rules;
using TicTacToe.Util;
using UnityEngine;

namespace TicTacToe.Services.Hint
{
    // Adapted from https://www.youtube.com/watch?v=trKjYdBASyQ
    public class MiniMax
    {
        private readonly IGameRules _gameRules;
        private IPlayer _otherPlayer;
        private IPlayer _player;

        public MiniMax(IGameRules gameRules)
        {
            _gameRules = gameRules;
        }

        public Vector2Int? GetBestMove(GameTile[,] board, IPlayer player, IPlayer otherPlayer)
        {
            _player = player;
            _otherPlayer = otherPlayer;
            var boardSize = board.GetLength(0);
            var bestMove = Vector2Int.zero;
            var foundPath = false;
            var bestScore = int.MinValue;
            for (var x = 0; x < boardSize; x++)
            for (var y = 0; y < boardSize; y++)
                if (!board[x, y].IsOccupied)
                {
                    board[x, y].SetPlayer(_otherPlayer);
                    var score = Minimax(board, true);
                    board[x, y].SetPlayer(null);
                    if (score > bestScore)
                    {
                        foundPath = true;
                        bestScore = score;
                        bestMove = new Vector2Int(x, y);
                    }
                }

            return foundPath ? bestMove : board.GetFirstFreeTile();
        }

        private int Minimax(GameTile[,] board, bool isMaximizing)
        {
            var winner = _gameRules.GetWinner(board, out var score);
            if (winner is not null)
                return winner == _player ? score : -score;
            ;

            if (_gameRules.IsTie(board))
                return 0;

            var boardSize = board.GetLength(0);
            if (isMaximizing)
            {
                var bestScore = int.MinValue;
                for (var x = 0; x < boardSize; x++)
                for (var y = 0; y < boardSize; y++)
                    if (!board[x, y].IsOccupied)
                    {
                        board[x, y].SetPlayer(_player);
                        var currentScore = Minimax(board, false);
                        board[x, y].SetPlayer(null);

                        bestScore = Mathf.Max(bestScore, currentScore);
                    }

                return bestScore;
            }
            else
            {
                var bestScore = int.MaxValue;
                for (var x = 0; x < boardSize; x++)
                for (var y = 0; y < boardSize; y++)
                    if (!board[x, y].IsOccupied)
                    {
                        board[x, y].SetPlayer(_otherPlayer);
                        var currentScore = Minimax(board, true);
                        board[x, y].SetPlayer(null);

                        bestScore = Mathf.Min(bestScore, currentScore);
                    }

                return bestScore;
            }
        }
    }
}