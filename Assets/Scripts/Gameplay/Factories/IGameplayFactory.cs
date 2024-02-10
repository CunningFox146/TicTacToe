using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Field;

namespace TicTacToe.Gameplay.Factories
{
    public interface IGameplayFactory
    {
        public UniTask<IGameField> CreateBackground();
    }
}