using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace TicTacToe.Services.Sounds
{
    [CreateAssetMenu(menuName = "Sound Info")]
    public class SoundInfo : ScriptableObject, ISoundInfo
    {
        [field: SerializeField] public List<AudioClip> Clips { get; private set; }
        [field: SerializeField] public float Volume { get; private set; }
        [field: SerializeField] public bool IsLoop { get; private set; }
        [field: SerializeField] public AudioMixerGroup MixerGroup { get; private set; }
    }
}