namespace TicTacToe.Services.Log
{
    public interface ILogService
    {
        void Log(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}
