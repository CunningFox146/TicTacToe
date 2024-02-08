using Cysharp.Threading.Tasks;

namespace TicTacToe.Infrastructure.States
{
    public class GameQuitState : IState
    {
        public UniTask Enter()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
            return UniTask.CompletedTask;
        }
    }
}