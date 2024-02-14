using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.Infrastructure.States;
using TicTacToe.Services.BoardPlayers;
using TicTacToe.Services.Difficulty;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.Randomizer;
using TicTacToe.Services.Skin;
using TicTacToe.StaticData.Gameplay;
using TicTacToe.UI.Factories;
using TicTacToe.UI.Services.Loading;
using TicTacToe.UI.ViewStack;

namespace TicTacToe.Gameplay.States
{
    public class GameplayInitState : IState
    {
        private readonly IPlayerFactory _playerFactory;
        private readonly IUserInterfaceFactory _userInterfaceFactory;
        private readonly IRandomService _random;
        private readonly IDifficultyService _difficulty;
        private readonly IGameplayFactory _factory;
        private readonly IGameBoardService _gameBoard;
        private readonly ISkinService _skinService;
        private readonly IStateMachine _gameStateMachine;
        private readonly ILoadingCurtainService _loadingCurtain;
        private readonly IViewStackService _viewStack;

        public GameplayInitState(IGameplayFactory factory, IViewStackService viewStack,
            ILoadingCurtainService loadingCurtain, IStateMachine gameStateMachine,
            IGameBoardService gameBoard, ISkinService skinService, IPlayerFactory
                playerFactory, IUserInterfaceFactory userInterfaceFactory, IRandomService random, IDifficultyService difficulty)
        {
            _factory = factory;
            _viewStack = viewStack;
            _loadingCurtain = loadingCurtain;
            _gameStateMachine = gameStateMachine;
            _gameBoard = gameBoard;
            _skinService = skinService;
            _playerFactory = playerFactory;
            _userInterfaceFactory = userInterfaceFactory;
            _random = random;
            _difficulty = difficulty;
        }

        public async UniTask Enter()
        {
            var settings = await _difficulty.GetSettings();
            
            await InitGameBoard(settings);
            await InitGameBoardController(settings);
            _viewStack.PushView(await _userInterfaceFactory.CreateHUDView()); 

            _loadingCurtain.HideLoadingCurtain();

            _gameStateMachine.Enter<GameplayLoopState>().Forget();
        }

        private async UniTask InitGameBoardController(IGameplaySettings settings)
        {
            var boardController = await _factory.CreateGameBoardController();
            boardController.SetBackground(await _skinService.LoadBackground());
            boardController.SetBoardSize(settings.FieldSize);
            await boardController.Init();
            _gameBoard.SetField(boardController);
        }

        private async UniTask InitGameBoard(IGameplaySettings settings)
        {
            _gameBoard.SetBoardSize(settings.FieldSize);

            var player = new []{ GetPlayer(), GetBotPlayer() };
            _random.Shuffle(player);

            var playerX = player[0];
            var playerO = player[1];

            playerX.PlayerSprite = await _skinService.LoadX();
            playerO.PlayerSprite = await _skinService.LoadO();

            playerX.Name = "X";
            playerO.Name = "O";
            
            _gameBoard.SetPlayers(player);
            _gameBoard.SetCountdownTime(settings.MoveDuration);
        }

        private IPlayer GetBotPlayer()
            => _playerFactory.Create<BotPlayer>();

        private IPlayer GetPlayer()
            => _playerFactory.Create<Player>();
    }
}