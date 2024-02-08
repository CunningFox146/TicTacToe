using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TicTacToe.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        UniTask<T> LoadAsset<T>(string bundleName, string assetName) where T : Object;
        UniTask<T> LoadAsset<T>(string assetName) where T : Object;
    }
}