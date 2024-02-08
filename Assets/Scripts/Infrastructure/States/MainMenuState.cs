using Cysharp.Threading.Tasks;
using TicTacToe.UI.Factories;
using TicTacToe.UI.Views;

namespace TicTacToe.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly IUserInterfaceFactory _userInterfaceFactory;

        public MainMenuState(IUserInterfaceFactory userInterfaceFactory)
        {
            _userInterfaceFactory = userInterfaceFactory;
        }
        
        public UniTask Enter()
        {
            var viewStack = _userInterfaceFactory.CreateViewStack();
            viewStack.PushView<MainMenuView>();
            
            return UniTask.CompletedTask;
        }
    }
}