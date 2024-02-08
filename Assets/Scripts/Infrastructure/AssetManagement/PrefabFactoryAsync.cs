using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TicTacToe.Infrastructure.AssetManagement
{
    public class PrefabFactoryAsync<TComponent> : IFactory<string, string, UniTask<TComponent>>
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;

        public PrefabFactoryAsync(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public async UniTask<TComponent> Create(string bundleName, string assetKey)
        {
            var prefab = await _assetProvider.LoadAsset<GameObject>(bundleName, assetKey);
            return _instantiator.InstantiatePrefab(prefab).GetComponent<TComponent>();;
        }
    }
}