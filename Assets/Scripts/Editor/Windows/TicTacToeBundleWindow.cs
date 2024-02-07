using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace TicTacToe.Editor.Windows
{
    public class TicTacToeBundleWindow : EditorWindow
    {
        private const string BuildPath = "Assets/StreamingAssets";

        private readonly Dictionary<string, Object> _objectsToPack = new Dictionary<string, Object>();
        private string _bundleName;
        private Button _buildButton;

        public void CreateGUI()
        {
            RenderWindow();
        }

        [MenuItem("Bundles/Build TicTacToe bundle")]
        private static void ShowWindow()
        {
            var window = GetWindow<TicTacToeBundleWindow>();
            window.titleContent = new GUIContent("Build TicTacToe bundle");
        }

        private void RenderWindow()
        {
            AddSpriteField("X Sprite", "xSprite");
            AddSpriteField("O Sprite", "oSprite");
            AddSpriteField("Background Sprite", "bgSprite");

            AddBundleNameField();
            AddBuildButton();
        }

        private void AddBuildButton()
        {
            _buildButton = new Button();
            _buildButton.text = "Build";
            _buildButton.clicked += OnBuildButtonClicked;
            rootVisualElement.Add(_buildButton);
            
            UpdateBuildButton();
        }
        
        private void Build()
        {
            AssetBundleBuild[] builds =
            {
                new AssetBundleBuild
                {
                    assetBundleName = _bundleName,
                    assetNames = _objectsToPack.Values.Select(AssetDatabase.GetAssetPath).ToArray(),
                    addressableNames = _objectsToPack.Keys.ToArray(),
                }
            };
            BuildPipeline.BuildAssetBundles(BuildPath, builds, BuildAssetBundleOptions.None,
                EditorUserBuildSettings.activeBuildTarget);
            AssetDatabase.Refresh();

            Debug.Log($"Asset Bundle built: {BuildPath}/{_bundleName}");
        }

        private void AddBundleNameField()
        {
            rootVisualElement.Add(new Label("Bundle name:"));
            var bundleNameField = new TextField();
            bundleNameField.RegisterCallback<ChangeEvent<string>>(OnBundleNameChange);
            rootVisualElement.Add(bundleNameField);
        }

        private void AddSpriteField(string label, string key)
        {
            rootVisualElement.Add(new Label(label));

            var field = new ObjectField();
            field.objectType = typeof(Sprite);
            field.RegisterValueChangedCallback(_ =>
            {
                _objectsToPack[key] = field.value;
                UpdateBuildButton();
            });

            _objectsToPack.Add(key, null);
            rootVisualElement.Add(field);
        }

        private void UpdateBuildButton()
        {
            _buildButton.SetEnabled(!string.IsNullOrWhiteSpace(_bundleName) &&
                                    _objectsToPack.Values.All(v => v));
        }

        private void OnBundleNameChange(ChangeEvent<string> evt)
        {
            _bundleName = evt.newValue;
            UpdateBuildButton();
        }

        private void OnBuildButtonClicked()
        {
            Build();
        }
    }
}