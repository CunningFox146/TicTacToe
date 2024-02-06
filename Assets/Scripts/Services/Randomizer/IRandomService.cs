namespace TicTacToe.Services.Randomizer
{
    public interface IRandomService
    {
        int GetInRange(int min, int max);
        bool GetBool();
    }
}