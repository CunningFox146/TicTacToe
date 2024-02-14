using System.Threading;
using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.SceneManagement;
using TicTacToe.Services.GameBoard;
using TicTacToe.UI.Services.Loading;
using TicTacToe.Util;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UniTask;

namespace TicTacToe.UI.ViewModels
{
    public class GameEndViewModel : IBindingContext
    {
        private const string HeaderWinString = "{0} Won!";
        private const string HeaderTieString = "Tie!";
        private const string MovesString = "They took <b>{0}</b> moves to win";
        private const string MovesTieString = "All players took <b>{0}</b> moves";
        private const string FreeTilesString = "Free tiles left: <b>{0}</b>";
        private readonly IGameBoardService _gameBoard;
        private readonly ILoadingCurtainService _loadingCurtain;

        private readonly ISceneLoader _sceneLoader;

        public IReadOnlyProperty<string> HeaderText { get; }
        public IReadOnlyProperty<string> MovesText { get; }
        public IReadOnlyProperty<string> FreeTilesText { get; }
        public ICommand PlayAgainCommand { get; }
        public ICommand MainMenuCommand { get; }
        
        public GameEndViewModel(ISceneLoader sceneLoader, ILoadingCurtainService loadingCurtain,
            IGameBoardService gameBoard)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameBoard = gameBoard;

            HeaderText = new ReadOnlyProperty<string>(GetHeaderText());
            MovesText = new ReadOnlyProperty<string>(GetMovesText());
            FreeTilesText = new ReadOnlyProperty<string>(GetFreeTilesText());

            PlayAgainCommand = new AsyncCommand(PlayAgain);
            MainMenuCommand = new AsyncCommand(MainMenu);
        }
        
        private string GetFreeTilesText() 
            => string.Format(FreeTilesString, _gameBoard.Board.GetFreeTilesCount());

        private string GetMovesText()
        {
            if (_gameBoard.IsTie())
                return string.Format(MovesTieString, _gameBoard.Board.GetOccupiedTilesCount());

            var winner = _gameBoard.GetWinner();
            var winnerTiles = 0;
            foreach (var gameTile in _gameBoard.Board)
                if (gameTile.Player == winner)
                    winnerTiles++;

            return string.Format(MovesString, winnerTiles);
        }

        private string GetHeaderText()
        {
            if (_gameBoard.IsTie())
                return HeaderTieString;
            var winner = _gameBoard.GetWinner();
            return string.Format(HeaderWinString, winner.Name);
        }

        private UniTask PlayAgain(CancellationToken cancellationToken)
        {
            _loadingCurtain.ShowLoadingCurtain();
            return _sceneLoader.LoadScene(SceneIndex.Gameplay);
        }

        private UniTask MainMenu(CancellationToken cancellationToken)
        {
            _loadingCurtain.ShowLoadingCurtain();
            return _sceneLoader.LoadScene(SceneIndex.MainMenu);
        }
    }
}