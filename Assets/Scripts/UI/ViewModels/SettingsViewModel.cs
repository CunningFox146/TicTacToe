using System;
using System.Collections.ObjectModel;
using System.Linq;
using TicTacToe.Services;
using TicTacToe.Services.Difficulty;
using UnityEngine;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;

namespace TicTacToe.UI.ViewModels
{
    public class SettingsViewModel : IBindingContext
    {
        private readonly IDifficultyService _difficultyService;
        public IReadOnlyProperty<ObservableCollection<string>> DifficultyList { get; }
        public IProperty<string> SelectedDifficulty { get; }

        public SettingsViewModel(IDifficultyService difficultyService)
        {
            _difficultyService = difficultyService;
            
            DifficultyList = new Property<ObservableCollection<string>>(GetDifficultiesList());
            SelectedDifficulty = new Property<string>(_difficultyService.CurrentDifficulty.ToString());

            SelectedDifficulty.ValueChanged += (q, s) => Debug.Log(s);
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