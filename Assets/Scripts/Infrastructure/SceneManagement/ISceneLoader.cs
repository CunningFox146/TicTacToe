using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace TicTacToe.Infrastructure.SceneManagement
{
    public interface ISceneLoader
    {
        UniTask LoadScene(SceneIndex sceneIndex, LoadSceneMode mode = LoadSceneMode.Single);
    }
}