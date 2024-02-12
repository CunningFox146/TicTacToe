using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Editor.Util;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.Services.Skin;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace TicTacToe.Editor.Windows
{
    public class TicTacToeBundleWindow : EditorWindow
    {

        private readonly Dictionary<string, Object> _objectsToPack = new();
        private string _bundleName;
        private Button _buildButton;
        private BuildTarget _platform;

        private void OnEnable()
        {
            _platform = EditorUserBuildSettings.activeBuildTarget;
        }

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
            AddSpriteField("X Sprite", SkinItemNames.X);
            AddSpriteField("O Sprite", SkinItemNames.O);
            AddSpriteField("Background Sprite", SkinItemNames.Background);
            AddPlatformPopup();

            AddBundleNameField();
            AddBuildButton();
        }

        private void AddPlatformPopup()
        {
            rootVisualElement.Add(new Label("Platform:"));

            var values = Enum.GetValues(typeof(BuildTarget)).Cast<BuildTarget>().ToList();
            var popup = new PopupField<BuildTarget>
            {
                choices = values
            };
            popup.value = EditorUserBuildSettings.activeBuildTarget;
            popup.RegisterValueChangedCallback(evt => _platform = evt.newValue);
            rootVisualElement.Add(popup);
        }

        private void AddBuildButton()
        {
            _buildButton = new Button();
            _buildButton.text = "Build";
            _buildButton.clicked += Build;
            rootVisualElement.Add(_buildButton);
            
            UpdateBuildButton();
        }

        private void Build()
            => BundleUtil.BuildAssetBundle(_bundleName, Application.streamingAssetsPath, _objectsToPack.Values, _objectsToPack.Keys, _platform);

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
    }
}