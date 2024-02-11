using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using TicTacToe.Gameplay.Field;
using UnityEngine;
using Zenject;

namespace TicTacToe.Gameplay.Line
{
    public class FieldLine : MonoBehaviour, IFieldLine
    {
        [SerializeField] private LineRenderer _renderer;
        private Material _material;

        public void SetTexture(Texture texture)
        {
            _material ??= new Material(_renderer.material);
            _material.mainTexture = texture;
            _renderer.material = _material;
        }

        public void SetPositions(Vector3[] positions)
        {
            foreach (var position in positions)
            {
                Debug.Log(position);
            }

            _renderer.positionCount = positions.Length;
            _renderer.SetPositions(positions);
        }

        public class Factory : PlaceholderFactory<string, string, UniTask<FieldLine>>
        {
        }
    }
}