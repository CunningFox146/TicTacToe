using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TicTacToe.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        UniTask LoadBundle(string bundleName);
        UniTask UnloadBundle(string bundleName);
        UniTask<T> LoadAsset<T>(string bundleName, string assetName) where T : Object;
        UniTask<T> LoadAsset<T>(string assetName) where T : Object;
        void UnloadAssets();
    }
}