using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TicTacToe.Infrastructure.AssetManagement
{
    public interface IAssetBundleProvider
    {
        UniTask<T> LoadAsset<T>(string bundlePath, string assetName) where T : Object;
    }
}