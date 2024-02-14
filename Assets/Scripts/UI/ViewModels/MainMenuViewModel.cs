using System.Threading;
using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.States;
using TicTacToe.UI.Factories;
using TicTacToe.UI.ViewStack;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UniTask;

namespace TicTacToe.UI.ViewModels
{
    public class MainMenuViewModel : IBindingContext
    {
        private readonly IStateMachine _gameStateMachine;
        private readonly IUserInterfaceFactory _userInterfaceFactory;
        private readonly IViewStackService _viewStack;
        
        public IProperty<int> Count { get; }
        public ICommand StartCommand { get; }
        public ICommand ReskinCommand { get; }
        public ICommand QuitCommand { get; }
        
        public MainMenuViewModel(IStateMachine gameStateMachine, IViewStackService viewStack,
            IUserInterfaceFactory userInterfaceFactory)
        {
            _gameStateMachine = gameStateMachine;
            _viewStack = viewStack;
            _userInterfaceFactory = userInterfaceFactory;

            Count = new Property<int>();
            StartCommand = new Command(StartGame);
            ReskinCommand = new AsyncCommand(ShowReskinView);
            QuitCommand = new Command(QuitGame);
        }


        private void StartGame()
        {
            _gameStateMachine.Enter<GameplayLoadState>();
        }

        private async UniTask ShowReskinView(CancellationToken cancellationToken)
        {
            _viewStack.PushView(await _userInterfaceFactory.CreateSkinPopupView());
        }

        private void QuitGame()
        {
            _gameStateMachine.Enter<GameQuitState>();
        }
    }
}