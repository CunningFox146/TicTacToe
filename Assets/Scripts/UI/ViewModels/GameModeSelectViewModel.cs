using TicTacToe.Infrastructure.States;
using TicTacToe.UI.Views;
using TicTacToe.UI.ViewStack;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;

namespace TicTacToe.UI.ViewModels
{
    public class GameModeSelectViewModel : IBindingContext
    {
        private readonly IStateMachine _gameStateMachine;
        private readonly IViewStackService _viewStack;
        
        public ICommand BotCommand { get; }
        public ICommand HumanCommand { get; }
        public ICommand CloseCommand { get; }

        public GameModeSelectViewModel(IStateMachine gameStateMachine, IViewStackService viewStack)
        {
            _gameStateMachine = gameStateMachine;
            _viewStack = viewStack;

            BotCommand = new Command(SelectBotMode);
            HumanCommand = new Command(SelectHumanMode);
            CloseCommand = new Command(Close);
        }

        private void SelectBotMode()
        {
            throw new System.NotImplementedException();
        }

        private void SelectHumanMode()
        {
            throw new System.NotImplementedException();
        }

        private void Close()
        {
            _viewStack.PopView();
        }
    }
}