using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TicTacToe.Services.Skin
{
    public interface ISkinService
    {
        string CurrentSkin { get; }
        UniTask SetSkin(string skin);
        UniTask<Sprite> LoadBackground();
        UniTask<Sprite> LoadX();
        UniTask<Sprite> LoadO();
    }
}