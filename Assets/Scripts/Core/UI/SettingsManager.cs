using DunkShot.Core.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace DunkShot.Core.UI
{
    public class SettingsManager : MonoBehaviour
    {
        [Header(" Elements ")]
        [SerializeField]
        private SoundManager _soundManager;
        [SerializeField]
        private TapticManager _tapticManager;
        [SerializeField]
        private Sprite _optionsOnSprite;
        [SerializeField]
        private Sprite _optionsOffSprite;
        [SerializeField]
        private Image _soundsButtonImage;
        [SerializeField]
        private Image _tapticsButtonImage;

        [Header(" Settings ")]
        private bool _soundsState = true;
        private bool _tapticsState = true;
        private const string SoundsKey = "Sounds";
        private const string TapticsKey = "Taptic";

        private void Awake()
        {
            _soundsState = PlayerPrefs.GetInt(SoundsKey, 1) == 1;
            _tapticsState = PlayerPrefs.GetInt(TapticsKey, 1) == 1;
        }

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            if (_soundsState)
                EnableSounds();
            else
                DisableSounds();
        }

        public void ChangeSoundsState()
        {
            if (_soundsState)
                DisableSounds();
            else
                EnableSounds();
            _soundsState = !_soundsState;
            PlayerPrefs.SetInt(SoundsKey, _soundsState ? 1 : 0);
        }

        public void ChangeTapticState()
        {
            if (_tapticsState)
                DisableTaptics();
            else
                EnableTaptics();
            _tapticsState = !_tapticsState;
            PlayerPrefs.SetInt(TapticsKey, _tapticsState ? 1 : 0);
        }

        private void EnableSounds() 
        {
            _soundManager.EnableSounds();
            _soundsButtonImage.sprite = _optionsOnSprite; 
        }

        private void DisableSounds()
        {
            _soundManager.DisableSounds();
            _soundsButtonImage.sprite = _optionsOffSprite;
        }

        private void EnableTaptics()
        {
            _tapticManager.EnableVibration();
            _tapticsButtonImage.sprite = _optionsOnSprite;
        }

        private void DisableTaptics()
        {
            _tapticManager.DisableVibration();
            _tapticsButtonImage.sprite = _optionsOffSprite;
        }
    }
}
