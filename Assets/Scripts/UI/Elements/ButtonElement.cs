using System;
using TicTacToe.Services.Sounds;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TicTacToe.UI.Elements
{
    [RequireComponent(typeof(Button))]
    public class ButtonElement : MonoBehaviour
    {
        private ISoundSource _soundSource;
        private Button _button;

        [Inject]
        public void Constructor(ISoundSource soundSource)
        {
            _soundSource = soundSource;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(PlayClickSound);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(PlayClickSound);
        }

        private void PlayClickSound()
        {
            _soundSource.PlaySound(SoundNames.Click);
        }
    }
}