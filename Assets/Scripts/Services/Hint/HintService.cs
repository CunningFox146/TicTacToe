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

        public Vector2Int GetBestMove(GameTile[,] board, IPlayer player, IPlayer other)
        {
            return _miniMax.GetBestMove(board, player, other);
        }
    }
}