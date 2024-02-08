using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TicTacToe.Editor.Util
{
    public static class BundleUtil
    {
        public static void BuildAssetBundle(string bundleName, string bundlePath, IEnumerable<Object> objectsToPack, IEnumerable<string> assetAlias)
        {
            AssetBundleBuild[] builds =
            {
                new()
                {
                    assetBundleName = bundleName,
                    assetNames = objectsToPack.Select(AssetDatabase.GetAssetPath).ToArray(),
                    addressableNames = assetAlias.ToArray(),
                }
            };
            BuildPipeline.BuildAssetBundles(bundlePath, builds, BuildAssetBundleOptions.None,
                EditorUserBuildSettings.activeBuildTarget);
            AssetDatabase.Refresh();

            Debug.Log($"Asset Bundle built: {bundlePath}/{bundleName}");
        }
    }
}