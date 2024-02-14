using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TicTacToe.Infrastructure.AssetManagement
{
    public class AssetBundleProvider : IAssetProvider
    {
        private readonly Dictionary<string, AssetBundle> _loadedBundles = new();

        public async UniTask LoadBundle(string bundleName)
        {
            if (_loadedBundles.ContainsKey(bundleName))
                return;
            
            var bundlePath = $"{Application.streamingAssetsPath}/{bundleName}";
            if (!File.Exists(bundlePath))
                throw new FileNotFoundException($"No such bundle: {bundlePath}");
            
            var bundle = await AssetBundle.LoadFromFileAsync(bundlePath).ToUniTask();
            _loadedBundles.Add(bundleName, bundle);
        }

        public UniTask UnloadBundle(string bundleName)
        {
            if (!_loadedBundles.TryGetValue(bundleName, out var bundle) || !bundle)
                return UniTask.CompletedTask;

            return bundle.UnloadAsync(true).ToUniTask();
        }

        public async UniTask<T> LoadAsset<T>(string bundleName, string assetName) where T : Object
        {
            try
            {
                var bundle = _loadedBundles[bundleName];
                if (bundle)
                    return await _loadedBundles[bundleName].LoadAssetAsync<T>(assetName).ToUniTask() as T;
            }
            catch (InvalidCastException ex)
            {
                Debug.LogException(ex);
            }

            return null;
        }

        public async UniTask<T> LoadAsset<T>(string assetName) where T : Object 
            => await LoadAsset<T>(BundleNames.GenericBundle, assetName);

        public void UnloadAssets() 
            => AssetBundle.UnloadAllAssetBundles(true);
    }
}