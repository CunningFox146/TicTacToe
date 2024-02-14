using System;
using Cysharp.Threading.Tasks;
using TicTacToe.Infrastructure.AssetManagement;
using TicTacToe.Services.Randomizer;
using UnityEngine;
using UnityEngine.Pool;

namespace TicTacToe.Services.Sounds
{
    public class SoundSource : ISoundSource
    {
        private readonly IRandomService _randomService;
        private readonly IAssetProvider _assetProvider;
        private readonly ObjectPool<AudioSource> _audioSources;

        public SoundSource(IRandomService randomService, IAssetProvider assetProvider)
        {
            _randomService = randomService;
            _assetProvider = assetProvider;
            _audioSources = new ObjectPool<AudioSource>(
                () => new GameObject("AudioSource", typeof(AudioSource)).GetComponent<AudioSource>(),
                audio => audio.gameObject.SetActive(true),
                audio => audio.gameObject.SetActive(false)
            );
        }

        public async UniTask LoadSoundBundle()
        {
            await _assetProvider.LoadBundle(SoundNames.SoundBundle);
        }

        public async UniTask PlaySound(string soundName, float delay = 0f)
        {
            var soundInfo = await LoadSoundInfo(soundName);
            await PlaySound(soundInfo, delay);
        }

        private async UniTask PlaySound(ISoundInfo soundInfo, float delay)
        {
            if (delay > 0f)
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
        
            var audio = _audioSources.Get();
            audio.outputAudioMixerGroup = soundInfo.MixerGroup;
            audio.loop = soundInfo.IsLoop;
            audio.volume = soundInfo.Volume;
            audio.clip = soundInfo.Clips[_randomService.GetInRange(0, soundInfo.Clips.Count - 1)];
            audio.Play();
            
            if (!soundInfo.IsLoop)
            {
                await UniTask.Delay(Mathf.FloorToInt(audio.clip.length * 1000));
                _audioSources.Release(audio);
            }
        }

        private async UniTask<ISoundInfo> LoadSoundInfo(string soundName)
            => await _assetProvider.LoadAsset<SoundInfo>(SoundNames.SoundBundle, soundName);
    }
}