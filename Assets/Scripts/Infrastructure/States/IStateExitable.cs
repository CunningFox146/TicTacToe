using Cysharp.Threading.Tasks;

namespace TicTacToe.Infrastructure.States
{
    internal interface IStateExitable
    {
        UniTask Exit();
    }
}