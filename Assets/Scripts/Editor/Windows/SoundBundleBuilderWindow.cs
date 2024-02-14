using UnityEditor;
using UnityEngine;

namespace TicTacToe.Editor.Windows
{
    public class SoundBundleBuilderWindow : BundleBuilderWindow
    {
        protected override void OnEnable()
        {
            settingsPath = "Assets/Settings/SoundObjects.asset";
            base.OnEnable();
            
        }
        
        [MenuItem("Bundles/Sound Bundle")]
        private static void ShowWindow()
        {
            var window = GetWindow<SoundBundleBuilderWindow>();
            window.titleContent = new GUIContent("Sound Bundle");
        }
    }
}