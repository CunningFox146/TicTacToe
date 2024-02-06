using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using UnityEngine;

namespace TicTacToe.Infrastructure.States
{
    public class GameBootstrapState : IState
    {
        private readonly IAssetProvider _assetProvider;

        public GameBootstrapState(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public async UniTask Enter()
        {
            var a = await _assetProvider.LoadAsset<Sprite>("Assets/StreamingAssets/123", "xSprite");
            Debug.Log(a.name);
        }
    }
}