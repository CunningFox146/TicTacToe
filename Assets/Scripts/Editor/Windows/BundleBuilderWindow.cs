using System.IO;
using TicTacToe.Editor.Util;
using TicTacToe.Infrastructure.AssetManagement;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Windows
{
    public class BundleBuilderWindow : EditorWindow
    {
        protected string settingsPath;
        private BundleObjects _bundleObjects;

        protected virtual void OnEnable()
        {
            _bundleObjects = LoadBundleObjects();
        }

        public void CreateGUI()
        {
            RenderWindow();
        }

        private BundleObjects LoadBundleObjects()
        {
            if (string.IsNullOrEmpty(settingsPath))
                return null;
            
            if (!File.Exists(settingsPath))
            {
                var settings = CreateInstance<BundleObjects>();
                AssetDatabase.CreateAsset(settings, settingsPath);
            }
            return AssetDatabase.LoadAssetAtPath<BundleObjects>(settingsPath);
        }

        private void RenderWindow()
        {
            AddBuildButton();
            AddBundleObjectsList();
        }
        
        private void Build() 
            => BundleUtil.BuildAssetBundle(_bundleObjects.BundleName, Application.streamingAssetsPath, _bundleObjects.ObjectsToPack.Values, _bundleObjects.ObjectsToPack.Keys, _bundleObjects.BuildTarget);

        private void AddBundleObjectsList()
        {
            if (_bundleObjects is null)
                return;
            
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