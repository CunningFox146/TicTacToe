using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.GameplayBg;

namespace TicTacToe.Gameplay.Factories
{
    public interface IGameplayFactory
    {
        public UniTask<IGameplayBackground> CreateBackground();
    }
}