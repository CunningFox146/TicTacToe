using Cysharp.Threading.Tasks;
using TicTacToe.StaticData.Gameplay;

namespace TicTacToe.Services.Difficulty
{
    public interface IDifficultyService
    {
        DifficultyLevel CurrentDifficulty { get; }
        void SetDifficulty(DifficultyLevel difficulty);
        UniTask<IGameplaySettings> GetSettings();
    }
}