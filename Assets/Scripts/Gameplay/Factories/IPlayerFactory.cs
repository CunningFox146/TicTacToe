using TicTacToe.Services.BoardPlayers;
using TicTacToe.Services.GameMode;

namespace TicTacToe.Gameplay.Factories
{
    public interface IPlayerFactory
    {
        TController Create<TController>() where TController : IPlayer;
        IPlayer[] GetPlayers(GameMode mode);
    }
}