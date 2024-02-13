using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.GameBoard.BoardPlayers;
using TicTacToe.Services.GameBoard.Rules;
using UnityEngine;

namespace TicTacToe.Services.Hint
{
    public class HintService : IHintService
    {
        private readonly MiniMax _miniMax;

        public HintService(IGameRules gameRules)
        {
            _miniMax = new MiniMax(gameRules);
        }

        public async UniTask<Vector2Int> GetBestMove(GameTile[,] sourceBoard, IPlayer player, IPlayer other)
        {
            var board = CloneBoard(sourceBoard);
            var task = Task<Vector2Int>.Factory.StartNew(() => _miniMax.GetBestMove(board, player, other));
            await task.AsUniTask();
            return task.Result;
        }

        private static GameTile[,] CloneBoard(GameTile[,] sourceBoard)
        {
            var size = sourceBoard.GetLength(0);
            var board = new GameTile[size, size];
            for (var x = 0; x < size; x++)
            for (var y = 0; y < size; y++)
                board[x, y] = (GameTile)sourceBoard[x, y].Clone();
            
            return board;
        }
    }
}