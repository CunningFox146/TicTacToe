using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.SceneManagement;
using TicTacToe.Services.GameBoard;
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
        public IProperty<bool> IsHintVisible { get; }
        public IProperty<Vector3> HintPosition { get; }
        public ICommand HintCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand ExitCommand { get; }
        
        public HUDViewModel(ISceneLoader sceneLoader, ILoadingCurtainService loadingCurtain, IGameBoardService gameBoard)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameBoard = gameBoard;

            IsHintVisible = new Property<bool>(false);
            HintPosition = new Property<Vector3>();
            
            HintCommand = new Command(ShowHint);
            UndoCommand = new Command(Undo);
            ExitCommand = new AsyncCommand(Exit);
        }

        private void ShowHint()
        {
            throw new NotImplementedException();
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
    }
}