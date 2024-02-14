using TicTacToe.Services.GameBoard.BoardPlayers;

namespace TicTacToe.Gameplay.Factories
{
    public interface IPlayerFactory
    {
        TController Create<TController>() where TController : IPlayer;
    }
}