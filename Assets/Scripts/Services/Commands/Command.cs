using System;

namespace TicTacToe.Services.Commands
{
    public class Command : ICommand
    {
        private readonly Action _runAction;
        private readonly Action _undoAction;

        public void Run() 
            => _runAction?.Invoke();

        public void Undo() 
            => _undoAction?.Invoke();

        public Command(Action runAction, Action undoAction, bool callRun = true)
        {
            _runAction = runAction;
            _undoAction = undoAction;
            
            if (callRun)
                Run();
        }
    }
}