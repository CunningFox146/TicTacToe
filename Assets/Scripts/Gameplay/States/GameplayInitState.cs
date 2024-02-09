using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Infrastructure.States;
using TicTacToe.UI.Services.Loading;
using TicTacToe.UI.ViewStack;

namespace TicTacToe.Gameplay.States
{
    public class GameplayInitState : IState
    {
        private readonly IGameplayFactory _factory;
        private readonly IViewStackService _viewStack;
        private readonly ILoadingCurtainService _loadingCurtain;

        public GameplayInitState(IGameplayFactory factory, IViewStackService viewStack, ILoadingCurtainService loadingCurtain)
        {
            _factory = factory;
            _viewStack = viewStack;
            _loadingCurtain = loadingCurtain;
        }
        
        public async UniTask Enter()
        {
            _viewStack.ClearStack();
            await _factory.CreateBackground();
            _loadingCurtain.HideLoadingCurtain();
        }
    }
}