namespace TicTacToe.Services.GameMode
{
    public interface IGameModeService
    {
        GameMode SelectedGameMode { get; }
        void SetGameMode(GameMode mode);
    }
}