using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEditor;
using UnityEngine;

namespace TicTacToe.Editor
{
    [CreateAssetMenu(menuName = "Asset Bundle/BundleObjects")]
    public class BundleObjects : ScriptableObject
    {
        [field: SerializeField] public string BundleName { get; private set; }
        [field: SerializeField] public BuildTarget BuildTarget { get; private set; }
        [SerializeField] private SerializedDictionary<string, Object> _objectsToPack;
        public Dictionary<string, Object> ObjectsToPack => _objectsToPack;
    }
}