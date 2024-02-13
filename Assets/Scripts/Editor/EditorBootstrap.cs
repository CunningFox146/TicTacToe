using UnityEditor;
using UnityEditor.SceneManagement;

namespace TicTacToe.Editor
{
    [InitializeOnLoad]
    public static class EditorBootstrap
    {
        private const string BootstrapScenePath = "Assets/Scenes/Bootstrap.unity";

        static EditorBootstrap()
        {
#if UNITY_INCLUDE_TESTS
            EditorSceneManager.playModeStartScene = null;
#else
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(BootstrapScenePath);
#endif
        }
    }
}