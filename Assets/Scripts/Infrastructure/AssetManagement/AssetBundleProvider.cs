using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TicTacToe.Infrastructure.AssetManagement
{
    public class AssetBundleProvider : IAssetProvider
    {
        private readonly Dictionary<string, AssetBundle> _loadedBundles = new();

        public async UniTask LoadBundle(string bundleName)
        {
            var bundle = await AssetBundle.LoadFromFileAsync($"{BundleNames.LocalBundlePath}/{bundleName}").ToUniTask();
            _loadedBundles.Add(bundleName, bundle);
        }
        
        public async UniTask<T> LoadAsset<T>(string bundleName, string assetName) where T : Object
            => await _loadedBundles[bundleName].LoadAssetAsync<T>(assetName).ToUniTask() as T;

        public async UniTask<T> LoadAsset<T>(string assetName) where T : Object 
            => await LoadAsset<T>(BundleNames.GenericBundle, assetName);

        private static async UniTask<AssetBundle> LoadLocalBundle(string bundlePath)
            => await AssetBundle.LoadFromFileAsync($"{BundleNames.LocalBundlePath}/{bundlePath}").ToUniTask();
    }
}