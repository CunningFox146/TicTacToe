using TicTacToe.Infrastructure.States;
using TicTacToe.UI.ViewStack;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;

namespace TicTacToe.UI.ViewModels
{
    public class MainMenuViewModel : IBindingContext
    {
        private readonly IStateMachine _gameStateMachine;
        private readonly IViewStackService _viewStack;
        public IProperty<int> Count { get; }
        public ICommand StartCommand { get; }
        public ICommand ReskinCommand { get; }
        public ICommand QuitCommand { get; }
        
        public MainMenuViewModel(IStateMachine gameStateMachine, IViewStackService viewStack)
        {
            _gameStateMachine = gameStateMachine;
            _viewStack = viewStack;

            Count = new Property<int>();
            StartCommand = new Command(StartGame);
            ReskinCommand = new Command(ShowReskinView);
            QuitCommand = new Command(QuitGame);
        }

        private void StartGame()
        {
            _gameStateMachine.Enter<GameplayLoadState>();
        }

        private void ShowReskinView()
        {
        }
        
        private void QuitGame()
        {
            _gameStateMachine.Enter<GameQuitState>();
        }
    }
}