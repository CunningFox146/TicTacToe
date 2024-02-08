using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.Infrastructure.SceneManagement;
using UnityEngine;

namespace TicTacToe.Infrastructure.States
{
    public class GameBootstrapState : IState
    {
        private readonly ISceneLoader _sceneLoader;

        public GameBootstrapState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public async UniTask Enter()
        {
            await _sceneLoader.LoadScene(SceneIndex.MainMenu);
        }
    }
}