using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace TicTacToe.Services.Sounds
{
    public interface ISoundInfo
    {
        List<AudioClip> Clips { get; }
        float Volume { get; }
        bool IsLoop { get; }
        bool IsSfx { get; }
    }
}