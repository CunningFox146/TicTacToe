using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.Infrastructure.States;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.GameBoard.BoardPlayers;
using TicTacToe.Services.Skin;
using TicTacToe.StaticData.Gameplay;
using TicTacToe.UI.Factories;
using TicTacToe.UI.Services.Loading;
using TicTacToe.UI.ViewStack;
using UnityEngine;

namespace TicTacToe.Gameplay.States
{
    public class GameplayInitState : IState
    {
        private readonly IAssetProvider _assetProvider;
        private readonly BoardControllerFactory _controllerFactory;
        private readonly IUserInterfaceFactory _userInterfaceFactory;
        private readonly IGameplayFactory _factory;
        private readonly IGameBoardService _gameBoard;
        private readonly ISkinService _skinService;
        private readonly IStateMachine _gameStateMachine;
        private readonly ILoadingCurtainService _loadingCurtain;
        private readonly IViewStackService _viewStack;

        public GameplayInitState(IGameplayFactory factory, IViewStackService viewStack,
            ILoadingCurtainService loadingCurtain, IAssetProvider assetProvider, IStateMachine gameStateMachine,
            IGameBoardService gameBoard, ISkinService skinService, BoardControllerFactory
                controllerFactory, IUserInterfaceFactory userInterfaceFactory)
        {
            _factory = factory;
            _viewStack = viewStack;
            _loadingCurtain = loadingCurtain;
            _assetProvider = assetProvider;
            _gameStateMachine = gameStateMachine;
            _gameBoard = gameBoard;
            _skinService = skinService;
            _controllerFactory = controllerFactory;
            _userInterfaceFactory = userInterfaceFactory;
        }

        public async UniTask Enter()
        {
            var settings = await _assetProvider.LoadAsset<GameplaySettings>(GameplayAssetNames.GameplaySettings);
            
            await InitGameBoard(settings);
            await InitGameField(settings);
            await _userInterfaceFactory.CreateHUDView(); 

            _loadingCurtain.HideLoadingCurtain();

            _gameStateMachine.Enter<GameplayLoopState>().Forget();
        }

        private async UniTask InitGameField(IGameplaySettings settings)
        {
            var field = await _factory.CreateGameField();
            field.SetBackground(await _skinService.LoadBackground());
            field.SetFieldSize(settings.FieldSize);
            await field.Init();
        }

        private async UniTask InitGameBoard(IGameplaySettings settings)
        {
            _gameBoard.SetBoardSize(settings.FieldSize);
            var playerX = _controllerFactory.Create<Player>();
            var playerO = _controllerFactory.Create<BotPlayer>();

            playerX.PlayerSprite = await _skinService.LoadX();
            playerO.PlayerSprite = await _skinService.LoadO();

            playerX.Name = "X";
            playerO.Name = "O";
            
            _gameBoard.SetPlayers(new IPlayer[] { playerX, playerO });
        }
    }
}