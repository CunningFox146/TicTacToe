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
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(BootstrapScenePath);
        }
    }
}