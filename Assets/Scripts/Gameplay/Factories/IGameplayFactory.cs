using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.BoardLine;
using TicTacToe.Gameplay.BoardTile;
using TicTacToe.Gameplay.GameBoard;

namespace TicTacToe.Gameplay.Factories
{
    public interface IGameplayFactory
    {
        public UniTask<IGameBoardController> CreateGameBoardController();
        public UniTask<IBoardLineController> CreateBoardLineController();
        public UniTask<IBoardTileController> CreateBoardTileController();
    }
}