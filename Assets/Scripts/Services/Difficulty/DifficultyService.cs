using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Factories;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.StaticData.Gameplay;

namespace TicTacToe.Services.Difficulty
{
    public class DifficultyService : IDifficultyService
    {
        private readonly IAssetProvider _assetProvider;

        private readonly Dictionary<DifficultyLevel, string> _settingsNames = new()
        {
            [DifficultyLevel.Easy] = GameplayAssetNames.EasyGameplaySettings,
            [DifficultyLevel.Normal] = GameplayAssetNames.NormalGameplaySettings,
            [DifficultyLevel.Hard] = GameplayAssetNames.HardGameplaySettings
        };

        public DifficultyLevel CurrentDifficulty { get; private set; } = DifficultyLevel.Normal;
        public DifficultyService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void SetDifficulty(DifficultyLevel difficulty) 
            => CurrentDifficulty = difficulty;

        public async UniTask<IGameplaySettings> GetSettings() 
            => await _assetProvider.LoadAsset<GameplaySettings>(_settingsNames[CurrentDifficulty]);
    }
}