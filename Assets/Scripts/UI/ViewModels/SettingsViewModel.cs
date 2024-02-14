using System;
using System.Collections.ObjectModel;
using TicTacToe.Services.Difficulty;
using TicTacToe.Services.Sounds;
using TicTacToe.UI.ViewStack;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;

namespace TicTacToe.UI.ViewModels
{
    public class SettingsViewModel : IBindingContext
    {
        private readonly IDifficultyService _difficultyService;
        private readonly IViewStackService _viewStack;
        private readonly ISoundSource _soundSource;

        public IReadOnlyProperty<ObservableCollection<string>> DifficultyList { get; }
        public IProperty<string> SelectedDifficulty { get; }
        public IProperty<bool> IsMusicEnabled { get; }
        public IProperty<bool> AreSoundsEnabled { get; }
        public ICommand CloseCommand { get; }
        

        public SettingsViewModel(IDifficultyService difficultyService, IViewStackService viewStack, ISoundSource soundSource)
        {
            _difficultyService = difficultyService;
            _viewStack = viewStack;
            _soundSource = soundSource;

            IsMusicEnabled = new Property<bool>(_soundSource.IsMusicEnabled);
            AreSoundsEnabled = new Property<bool>(_soundSource.IsSfxEnabled);
            SelectedDifficulty = new Property<string>(_difficultyService.CurrentDifficulty.ToString());
            DifficultyList = new Property<ObservableCollection<string>>(GetDifficultiesList());
            
            CloseCommand = new Command(Close);

            SelectedDifficulty.ValueChanged += OnDifficultyChanged;
            SelectedDifficulty.Value = _difficultyService.CurrentDifficulty.ToString();
            
            IsMusicEnabled.ValueChanged += OnMusicEnabledChanged;
            AreSoundsEnabled.ValueChanged += OnSoundsEnabledChanged;
        }

        private void Close()
        {
            _viewStack.PopView();
        }

        private static ObservableCollection<string> GetDifficultiesList()
        {
            var difficulties = new ObservableCollection<string>();
            foreach (DifficultyLevel difficulty in Enum.GetValues(typeof(DifficultyLevel)))
            {
                difficulties.Add(difficulty.ToString());
            }

            return difficulties;
        }
        
        private void OnMusicEnabledChanged(object sender, bool enabled) 
            => _soundSource.SetMusicEnabled(enabled);
        
        private void OnSoundsEnabledChanged(object sender, bool enabled) 
            => _soundSource.SetSfxEnabled(enabled);

        private void OnDifficultyChanged(object sender, string difficultyName)
        {
            if (Enum.TryParse<DifficultyLevel>(difficultyName, out var difficulty))
                _difficultyService.SetDifficulty(difficulty);
        }
    }
}