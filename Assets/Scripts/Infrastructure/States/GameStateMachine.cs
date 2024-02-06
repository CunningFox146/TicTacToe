using TicTacToe.Services.Log;

namespace TicTacToe.Infrastructure.States
{
    public class GameStateMachine : StateMachine
    {
        public GameStateMachine(ILogService log) : base(log)
        {
        }
    }
}