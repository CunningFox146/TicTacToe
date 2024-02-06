using Cysharp.Threading.Tasks;

namespace TicTacToe.Infrastructure.States
{
    public interface IState
    {
        UniTask Enter();
    }
}