using Cysharp.Threading.Tasks;
using UnityEditor;

namespace TicTacToe.Infrastructure.States
{
    public class GameQuitState : IState
    {
        public UniTask Enter()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
            return UniTask.CompletedTask;
        }
    }
}