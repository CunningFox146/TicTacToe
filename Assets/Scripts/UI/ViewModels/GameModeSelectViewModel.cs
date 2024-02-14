using TicTacToe.Infrastructure.States;
using TicTacToe.Services.GameMode;
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
        private readonly IGameModeService _gameMode;

        public ICommand BotCommand { get; }
        public ICommand HumanCommand { get; }
        public ICommand CloseCommand { get; }

        public GameModeSelectViewModel(IStateMachine gameStateMachine, IViewStackService viewStack, IGameModeService gameMode)
        {
            _gameStateMachine = gameStateMachine;
            _viewStack = viewStack;
            _gameMode = gameMode;

            BotCommand = new Command(SelectBotMode);
            HumanCommand = new Command(SelectHumanMode);
            CloseCommand = new Command(Close);
        }

        private void SelectBotMode() 
            => SelectModeAndStartGameplay(GameMode.Bot);

        private void SelectHumanMode()
            => SelectModeAndStartGameplay(GameMode.Human);

        private void Close()
        {
            _viewStack.PopView();
        }

        private void SelectModeAndStartGameplay(GameMode mode)
        {
            _gameMode.SetGameMode(mode);
            _gameStateMachine.Enter<GameplayLoadState>();
        }
    }
}