using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TicTacToe.Infrastructure.AssetManagement
{
    public class AssetBundleProvider : IAssetBundleProvider
    {
        public async UniTask<T> LoadAsset<T>(string bundlePath, string assetName) where T : Object
        {
            var bundle = await AssetBundle.LoadFromFileAsync(bundlePath).ToUniTask();
            return await bundle.LoadAssetAsync<T>(assetName).ToUniTask() as T;
        }
    }
}