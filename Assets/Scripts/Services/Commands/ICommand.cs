namespace TicTacToe.Services.Commands
{
    public interface ICommand
    {
        void Run();
        void Undo();
    }
}