using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TicTacToe.Infrastructure.AssetManagement
{
    public class AssetBundleProvider : IAssetProvider
    {
        private readonly Dictionary<string, AssetBundle> _loadedBundles = new();

        public async UniTask LoadBundle(string bundleName)
        {
            var bundle = await AssetBundle.LoadFromFileAsync($"{Application.streamingAssetsPath}/{bundleName}").ToUniTask();
            _loadedBundles.Add(bundleName, bundle);
        }

        public UniTask UnloadBundle(string bundleName)
        {
            if (!_loadedBundles.TryGetValue(bundleName, out var bundle))
                return UniTask.CompletedTask;

            return bundle.UnloadAsync(true).ToUniTask();
        }

        public async UniTask<T> LoadAsset<T>(string bundleName, string assetName) where T : Object
            => await _loadedBundles[bundleName].LoadAssetAsync<T>(assetName).ToUniTask() as T;

        public async UniTask<T> LoadAsset<T>(string assetName) where T : Object 
            => await LoadAsset<T>(BundleNames.GenericBundle, assetName);

        public void UnloadAssets() 
            => AssetBundle.UnloadAllAssetBundles(true);
    }
}