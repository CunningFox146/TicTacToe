using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace TicTacToe.Infrastructure.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public UniTask LoadScene(SceneIndex sceneIndex, LoadSceneMode mode = LoadSceneMode.Single)
            => SceneManager.LoadSceneAsync((int)sceneIndex).ToUniTask();
    }
}