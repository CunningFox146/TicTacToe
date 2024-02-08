using TicTacToe.Editor.Util;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Windows
{
    public class BundleBuilderWindow : EditorWindow
    {
        private const string BuildPath = "Assets/StreamingAssets";
        private BundleObjects _bundleObjects;
        private Button _buildButton;

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

        private void RenderWindow()
        {
            AddBundleObjectsField();
            AddBuildButton();
        }
        
        private void Build() 
            => BundleUtil.BuildAssetBundle(_bundleObjects.BundleName, BuildPath, _bundleObjects.ObjectsToPack.Values, _bundleObjects.ObjectsToPack.Keys);

        private void AddBundleObjectsField()
        {
            rootVisualElement.Add(new Label("Objects to pack:"));

            var field = new ObjectField();
            field.objectType = typeof(BundleObjects);
            field.RegisterValueChangedCallback(evt =>
            {
                _bundleObjects = (BundleObjects)evt.newValue;
                UpdateBuildButton();
            });
            rootVisualElement.Add(field);
        }
        
        private void AddBuildButton()
        {
            _buildButton = new Button();
            _buildButton.text = "Build";
            _buildButton.clicked += Build;
            rootVisualElement.Add(_buildButton);
            
            UpdateBuildButton();
        }
        
        private void UpdateBuildButton()
        {
            _buildButton.SetEnabled(_bundleObjects);
        }
    }
}