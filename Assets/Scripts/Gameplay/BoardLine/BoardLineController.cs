using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TicTacToe.Gameplay.BoardLine
{
    public class BoardLineController : MonoBehaviour, IBoardLineController
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
            _renderer.positionCount = positions.Length;
            _renderer.SetPositions(positions);
        }

        public class Factory : PlaceholderFactory<string, string, UniTask<BoardLineController>>
        {
        }
    }
}