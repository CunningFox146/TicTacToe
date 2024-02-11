using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Field;
using TicTacToe.Gameplay.Line;
using TicTacToe.Gameplay.Tile;

namespace TicTacToe.Gameplay.Factories
{
    public interface IGameplayFactory
    {
        public UniTask<IGameField> CreateGameField();
        public UniTask<IFieldLine> CreateFieldLine();
        public UniTask<IFieldTile> CreateFieldTile();
    }
}