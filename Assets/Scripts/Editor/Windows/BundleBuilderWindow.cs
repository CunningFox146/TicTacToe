using System.IO;
using TicTacToe.Editor.Util;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Windows
{
    public class BundleBuilderWindow : EditorWindow
    {
        private const string SettingsPath = "Assets/Settings/BundleObjects.asset";
        private const string BuildPath = "Assets/StreamingAssets";
        private BundleObjects _bundleObjects;

        private void OnEnable()
        {
            _bundleObjects = LoadBundleObjects();
        }

        public void CreateGUI()
        {
            RenderWindow();
        }

        [MenuItem("Bundles/Build Bundle")]
        private static void ShowWindow()
        {
            var window = GetWindow<BundleBuilderWindow>();
            window.titleContent = new GUIContent("Build Bundle");
        }
        
        private static BundleObjects LoadBundleObjects()
        {
            if (!File.Exists(SettingsPath))
            {
                var settings = CreateInstance<BundleObjects>();
                AssetDatabase.CreateAsset(settings, SettingsPath);
            }
            return AssetDatabase.LoadAssetAtPath<BundleObjects>(SettingsPath);
        }

        private void RenderWindow()
        {
            AddBuildButton();
            AddBundleObjectsList();
        }
        
        private void Build() 
            => BundleUtil.BuildAssetBundle(_bundleObjects.BundleName, BuildPath, _bundleObjects.ObjectsToPack.Values, _bundleObjects.ObjectsToPack.Keys);

        private void AddBundleObjectsList()
        {
            var inspector = new InspectorElement();
            inspector.Bind(new SerializedObject(_bundleObjects));
            rootVisualElement.Add(inspector);
        }

        private void AddBuildButton()
        {
            var button = new Button();
            button.text = "Build";
            button.clicked += Build;
            rootVisualElement.Add(button);
        }
    }
}