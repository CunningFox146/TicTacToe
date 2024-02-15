using Cysharp.Threading.Tasks;

namespace TicTacToe.Services.Sounds
{
    public interface ISoundSource
    {
        public bool IsSfxEnabled { get; }
        public bool IsMusicEnabled { get; }
        
        UniTask Init();
        bool IsPlayingSound(string alias);
        UniTask PlaySound(string soundName, string alias = null, float delay = 0f);
        void KillSound(string soundName);

        void SetMusicEnabled(bool isEnabled);
        void SetSfxEnabled(bool isEnabled);
    }
}