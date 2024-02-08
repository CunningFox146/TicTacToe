using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TicTacToe.Infrastructure.AssetManagement
{
    public class AssetBundleProvider : IAssetProvider
    {
        public async UniTask<T> LoadAsset<T>(string bundleName, string assetName) where T : Object
        {
            var bundle = await LoadLocalBundle(bundleName);
            return await bundle.LoadAssetAsync<T>(assetName).ToUniTask() as T;
        }

        public async UniTask<T> LoadAsset<T>(string assetName) where T : Object
        {
            var bundle = await LoadLocalBundle(BundleNames.GenericBundleName);
            return await bundle.LoadAssetAsync<T>(assetName).ToUniTask() as T;
        }

        private static async UniTask<AssetBundle> LoadLocalBundle(string bundlePath)
        {
            return await AssetBundle.LoadFromFileAsync($"{BundleNames.LocalBundlePath}/{bundlePath}").ToUniTask();
        }
    }
}