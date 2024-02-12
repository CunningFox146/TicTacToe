using TicTacToe.Services.GameBoard.BoardPlayers;

namespace TicTacToe.Services.GameBoard.Rules
{
    public interface IGameRules
    {
        IPlayer GetWinner(GameTile[,] board, out int score);
        bool IsTie(GameTile[,] board);
        int GetBoardScore(GameTile[,] board);
    }
}