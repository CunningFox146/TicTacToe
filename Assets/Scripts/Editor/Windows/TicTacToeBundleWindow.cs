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
        private Button _buildButton;
        private TextField _bundleNameField;

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

        private void Build()
        {
            AssetBundleBuild[] builds =
            {
                new AssetBundleBuild
                {
                    assetBundleName = _bundleNameField.text,
                    assetNames = _objectsToPack.Values.Select(AssetDatabase.GetAssetPath).ToArray(),
                    addressableNames = _objectsToPack.Keys.ToArray(),
                }
            };
            BuildPipeline.BuildAssetBundles(BuildPath, builds, BuildAssetBundleOptions.None,
                EditorUserBuildSettings.activeBuildTarget);
            AssetDatabase.Refresh();

            Debug.Log($"Asset Bundle built: {BuildPath}/{_bundleNameField.text}");
        }

        private void RenderWindow()
        {
            var root = rootVisualElement;

            AddSpriteField("X Sprite", "xSprite");
            AddSpriteField("O Sprite", "oSprite");
            AddSpriteField("Background Sprite", "bgSprite");

            root.Add(new Label("Bundle name:"));
            _bundleNameField = new TextField();
            _bundleNameField.RegisterCallback<ChangeEvent<string>>(OnBundleNameChange);
            root.Add(_bundleNameField);

            _buildButton = new Button
            {
                text = "Build"
            };
            _buildButton.clicked += OnBuildButtonClicked;
            root.Add(_buildButton);

            UpdateBuildButton();
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

        private void OnBundleNameChange(ChangeEvent<string> evt)
        {
            UpdateBuildButton();
        }

        private void UpdateBuildButton()
        {
            _buildButton.SetEnabled(!string.IsNullOrWhiteSpace(_bundleNameField.text) &&
                                    _objectsToPack.Values.All(v => v));
        }

        private void OnBuildButtonClicked()
        {
            Build();
        }
    }
}