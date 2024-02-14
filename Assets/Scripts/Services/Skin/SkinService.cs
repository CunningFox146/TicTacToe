using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using UnityEngine;

namespace TicTacToe.Services.Skin
{
    public class SkinService : ISkinService
    {
        private readonly IAssetProvider _assetProvider;

        public string CurrentSkin { get; private set; }

        public SkinService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask SetSkin(string skin)
        {
            if (CurrentSkin == skin)
                return;
            
            if (!string.IsNullOrEmpty(CurrentSkin))
                await _assetProvider.UnloadBundle(CurrentSkin);

            CurrentSkin = skin;
            await _assetProvider.LoadBundle(CurrentSkin);
        }

        public UniTask<Sprite> LoadBackground() 
            => _assetProvider.LoadAsset<Sprite>(CurrentSkin, SkinItemNames.Background);

        public UniTask<Sprite> LoadX() 
            => _assetProvider.LoadAsset<Sprite>(CurrentSkin, SkinItemNames.X);

        public UniTask<Sprite> LoadO() 
            => _assetProvider.LoadAsset<Sprite>(CurrentSkin, SkinItemNames.O);
    }
}