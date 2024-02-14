using UnityEditor;
using UnityEngine;

namespace TicTacToe.Editor.Windows
{
    public class GenericBundleBuilderWindow : BundleBuilderWindow
    {
        protected override void OnEnable()
        {
            settingsPath = "Assets/Settings/BundleObjects.asset";
            base.OnEnable();
        }
        
        [MenuItem("Bundles/Generic Bundle")]
        private static void ShowWindow()
        {
            var window = GetWindow<GenericBundleBuilderWindow>();
            window.titleContent = new GUIContent("Generic Bundle");
        }
    }
}