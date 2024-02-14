using Cysharp.Threading.Tasks;

namespace TicTacToe.Services.Sounds
{
    public interface ISoundSource
    {
        UniTask LoadSoundBundle();
        UniTask PlaySound(string soundName, float delay = 0f);
    }
}