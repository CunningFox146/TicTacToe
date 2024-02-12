using TicTacToe.Services.GameBoard.Rules;
using UnityEngine;

namespace TicTacToe.Services.GameBoard.BoardPlayers
{
    public class MiniMax
    {
        private readonly IGameRules _gameRules;
        private readonly IPlayer _player;
        private IPlayer _otherPlayer;

        public MiniMax(IGameRules gameRules, IPlayer player)
        {
            _gameRules = gameRules;
            _player = player;
        }

        private int Minimax(GameTile[,] board, bool isMaximizing)
        {
            var winner = _gameRules.GetWinner(board, out var score);
            if (winner is not null)
                return score;

            if (_gameRules.IsTie(board))
                return 0;
            
            var boardSize = board.GetLength(0);
            if (isMaximizing)
            {
                var bestScore = int.MinValue;
                for (var x = 0; x < boardSize; x++)
                {
                    for (var y = 0; y < boardSize; y++)
                    {
                        if (!board[x, y].IsOccupied)
                        {
                            board[x, y].SetPlayer(_player);
                            var currentScore = Minimax(board, false);
                            board[x, y].SetPlayer(null);

                            bestScore = Mathf.Max(bestScore, currentScore);
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                var bestScore = int.MaxValue;
                for (var x = 0; x < boardSize; x++)
                {
                    for (var y = 0; y < boardSize; y++)
                    {
                        if (!board[x, y].IsOccupied)
                        {
                            board[x, y].SetPlayer(_otherPlayer);
                            var currentScore = Minimax(board, true);
                            board[x, y].SetPlayer(null);

                            bestScore = Mathf.Min(bestScore, currentScore);
                        }
                    }
                }
                return bestScore;
            }
        }

        public Vector2Int GetBestTurn(GameTile[,] board, IPlayer otherPlayer)
        {
            _otherPlayer = otherPlayer;
            var boardSize = board.GetLength(0);
            var bestMove = Vector2Int.zero;
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
                        bestScore = score;
                        bestMove = new Vector2Int(x, y);
                    }
                }

            return bestMove;
        }
    }
}