using TicTacToe.Services.BoardPlayers;
using TicTacToe.Services.GameBoard;

namespace TicTacToe.Services.Rules
{
    public interface IGameRules
    {
        IPlayer GetWinner(GameTile[,] board);
        bool IsTie(GameTile[,] board);
        int GetBoardScore(GameTile[,] board, IPlayer player);
    }
}