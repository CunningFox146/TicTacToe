namespace TicTacToe.Services.Randomizer
{
    public interface IRandomService
    {
        int GetInRange(int min, int max);
        void Shuffle<T>(T[] array);
        bool GetBool();
    }
}