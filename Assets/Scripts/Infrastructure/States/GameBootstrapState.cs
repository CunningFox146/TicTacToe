using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using UnityEngine;

namespace TicTacToe.Infrastructure.States
{
    public class GameBootstrapState : IState
    {
        private readonly IAssetBundleProvider _assetBundleProvider;

        public GameBootstrapState(IAssetBundleProvider assetBundleProvider)
        {
            _assetBundleProvider = assetBundleProvider;
        }
        
        public async UniTask Enter()
        {
            var a = await _assetBundleProvider.LoadAsset<Sprite>("Assets/StreamingAssets/123", "xSprite");
            Debug.Log(a.name);
        }
    }
}