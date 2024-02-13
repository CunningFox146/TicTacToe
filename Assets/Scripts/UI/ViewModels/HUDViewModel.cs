using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.SceneManagement;
using TicTacToe.Services.GameBoard;
using TicTacToe.Services.Hint;
using TicTacToe.UI.Services.Loading;
using UnityEngine;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UniTask;

namespace TicTacToe.UI.ViewModels
{
    public class HUDViewModel : IBindingContext
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtainService _loadingCurtain;
        private readonly IGameBoardService _gameBoard;
        private readonly IHintService _hintService;
        private readonly Camera _mainCamera;
        
        public IProperty<bool> IsHintVisible { get; }
        public IProperty<Vector3> HintPosition { get; }
        public IProperty<float> Countdown { get; }
        public ICommand HintCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand ExitCommand { get; }
        
        public HUDViewModel(ISceneLoader sceneLoader, ILoadingCurtainService loadingCurtain, IGameBoardService gameBoard, IHintService hintService, Camera mainCamera)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameBoard = gameBoard;
            _hintService = hintService;
            _mainCamera = mainCamera;

            IsHintVisible = new Property<bool>(false);
            HintPosition = new Property<Vector3>();
            Countdown = new Property<float>();
            
            HintCommand = new AsyncCommand(ShowHint);
            UndoCommand = new Command(Undo);
            ExitCommand = new AsyncCommand(Exit);
            
            gameBoard.CountdownStarted += OnCountdownStarted;
        }

        private async UniTask ShowHint(CancellationToken token)
        {
            var otherPlayer = _gameBoard.Players.First(p => p != _gameBoard.CurrentPlayer);
            var move = await _hintService.GetBestMove(_gameBoard.Board, _gameBoard.CurrentPlayer, otherPlayer);
            var tile = _gameBoard.Field.GetTile(move);
            HintPosition.Value = tile.GetScreenPosition(_mainCamera);
            IsHintVisible.Value = true;
            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: token);
            IsHintVisible.Value = false;
        }

        private void Undo()
        {
            // Undo two times so that we also undo bots turn
            for (int i = 0; i < 2; i++)
            {
                _gameBoard.Undo();
            }
        }

        private UniTask Exit(CancellationToken cancellationToken)
        {
            _loadingCurtain.ShowLoadingCurtain();
            return _sceneLoader.LoadScene(SceneIndex.MainMenu);
        }

        private async void StartCountdown(float time)
        {
            while (time > 0)
            {
                time -= Time.deltaTime;
                Countdown.Value = time;
                await Task.Yield();
            }
        }
        
        private void OnCountdownStarted(float time)
            => StartCountdown(time);
    }
}