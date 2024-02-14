using Cysharp.Threading.Tasks;

namespace TicTacToe.Services.Sounds
{
    public interface ISoundSource
    {
        public bool IsSfxEnabled { get; }
        public bool IsMusicEnabled { get; }
        
        UniTask Init();
        UniTask PlaySound(string soundName, float delay = 0f);

        void SetMusicEnabled(bool isEnabled);
        void SetSfxEnabled(bool isEnabled);
    }
}