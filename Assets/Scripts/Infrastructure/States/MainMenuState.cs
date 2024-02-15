using Cysharp.Threading.Tasks;
using TicTacToe.Services.Sounds;
using TicTacToe.UI.Factories;
using TicTacToe.UI.Services.Loading;
using TicTacToe.UI.ViewStack;

namespace TicTacToe.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private const string MainMenuMusicAlias = "MainMenuMusic";
        
        private readonly ILoadingCurtainService _loadingCurtain;
        private readonly ISoundSource _soundSource;
        private readonly IUserInterfaceFactory _userInterfaceFactory;
        private readonly IViewStackService _viewStackService;

        public MainMenuState(IUserInterfaceFactory userInterfaceFactory, IViewStackService viewStackService,
            ILoadingCurtainService loadingCurtain, ISoundSource soundSource)
        {
            _userInterfaceFactory = userInterfaceFactory;
            _viewStackService = viewStackService;
            _loadingCurtain = loadingCurtain;
            _soundSource = soundSource;
        }

        public async UniTask Enter()
        {
            if (!_soundSource.IsPlayingSound(MainMenuMusicAlias))
                _soundSource.PlaySound(SoundNames.MainMusic, MainMenuMusicAlias).Forget();
            
            _viewStackService.PushView(await _userInterfaceFactory.CreateMainMenuView());
            _loadingCurtain.HideLoadingCurtain();
        }
    }
}