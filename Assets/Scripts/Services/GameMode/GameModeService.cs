namespace TicTacToe.Services.GameMode
{
    public class GameModeService : IGameModeService
    {
        public GameMode SelectedGameMode { get; private set; }

        public void SetGameMode(GameMode mode)
        {
            SelectedGameMode = mode;
        }
    }
}