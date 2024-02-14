using System;
using System.Collections.ObjectModel;
using TicTacToe.Services.Difficulty;
using TicTacToe.UI.ViewStack;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;

namespace TicTacToe.UI.ViewModels
{
    public class SettingsViewModel : IBindingContext
    {
        private readonly IDifficultyService _difficultyService;
        private readonly IViewStackService _viewStack;
        public IReadOnlyProperty<ObservableCollection<string>> DifficultyList { get; }
        public IProperty<string> SelectedDifficulty { get; }
        public ICommand CloseCommand { get; }
        

        public SettingsViewModel(IDifficultyService difficultyService, IViewStackService viewStack)
        {
            _difficultyService = difficultyService;
            _viewStack = viewStack;

            DifficultyList = new Property<ObservableCollection<string>>(GetDifficultiesList());
            SelectedDifficulty = new Property<string>(_difficultyService.CurrentDifficulty.ToString());
            CloseCommand = new Command(Close);

            SelectedDifficulty.ValueChanged += OnDifficultyChanged;
            SelectedDifficulty.Value = _difficultyService.CurrentDifficulty.ToString();
        }

        private void OnDifficultyChanged(object sender, string difficultyName)
        {
            if (Enum.TryParse<DifficultyLevel>(difficultyName, out var difficulty))
                _difficultyService.SetDifficulty(difficulty);
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
    }
}