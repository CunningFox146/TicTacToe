using Cysharp.Threading.Tasks;

namespace TicTacToe.Infrastructure.States
{
    public class GameBootstrapState : IState
    {
        public UniTask Enter()
        {
            return UniTask.CompletedTask;
        }
    }
}