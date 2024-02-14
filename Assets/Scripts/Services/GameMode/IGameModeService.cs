namespace TicTacToe.Services.GameMode
{
    public interface IGameModeService
    {
        GameMode SelectedGameMode { get; }
        bool CanUseControls { get; }
        void SetGameMode(GameMode mode);
    }
}