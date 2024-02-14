using System;
using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.Services.Randomizer;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace TicTacToe.Services.Sounds
{
    public class SoundSource : ISoundSource
    {
        private const string VolumeSfx = "VolumeSFX";
        private const string VolumeMusic = "VolumeMusic";
        private const string SfxGroup = "SFX";
        private const string MusicGroup = "Music";
        
        private readonly IRandomService _randomService;
        private readonly IAssetProvider _assetProvider;
        private readonly ObjectPool<AudioSource> _audioSources;
        private readonly AudioMixer _audioMixer;

        public bool IsSfxEnabled { get; private set; } = true;
        public bool IsMusicEnabled { get; private set; } = true;

        public SoundSource(IRandomService randomService, IAssetProvider assetProvider, AudioMixer audioMixer)
        {
            _randomService = randomService;
            _assetProvider = assetProvider;
            _audioMixer = audioMixer;

            _audioSources = new ObjectPool<AudioSource>(
                CreateAudioSource,
                audio => audio.gameObject.SetActive(true),
                audio => audio.gameObject.SetActive(false)
            );
        }

        public async UniTask Init()
        {
            await _assetProvider.LoadBundle(SoundNames.SoundBundle);
        }

        public async UniTask PlaySound(string soundName, float delay = 0f)
        {
            var soundInfo = await LoadSoundInfo(soundName);
            await PlaySound(soundInfo, delay);
        }

        public void SetSfxEnabled(bool isEnabled)
        {
            IsSfxEnabled = isEnabled;
            _audioMixer.SetFloat(VolumeSfx, IsSfxEnabled ? 0f : -80f);
        }
        
        public void SetMusicEnabled(bool isEnabled)
        {
            IsMusicEnabled = isEnabled;
            _audioMixer.SetFloat(VolumeMusic, IsMusicEnabled ? 0f : -80f);
        }

        private async UniTask PlaySound(ISoundInfo soundInfo, float delay)
        {
            if (delay > 0f)
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
        
            var audio = _audioSources.Get();
            audio.outputAudioMixerGroup = _audioMixer.FindMatchingGroups(soundInfo.IsSfx ? SfxGroup : MusicGroup)[0];
            audio.loop = soundInfo.IsLoop;
            audio.volume = soundInfo.Volume;
            audio.clip = soundInfo.Clips[_randomService.GetInRange(0, soundInfo.Clips.Count - 1)];
            audio.name = audio.clip.name;
            audio.Play();
            
            if (!soundInfo.IsLoop)
            {
                await UniTask.Delay(Mathf.FloorToInt(audio.clip.length * 1000));
                _audioSources.Release(audio);
            }
        }

        private async UniTask<ISoundInfo> LoadSoundInfo(string soundName)
            => await _assetProvider.LoadAsset<SoundInfo>(SoundNames.SoundBundle, soundName);

        private static AudioSource CreateAudioSource()
        {
            var source = new GameObject("AudioSource", typeof(AudioSource));
            Object.DontDestroyOnLoad(source);
            return source.GetComponent<AudioSource>();
        }
    }
}