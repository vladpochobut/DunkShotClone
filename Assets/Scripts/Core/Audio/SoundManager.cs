using DunkShot.Core.Ball;
using DunkShot.Core.Basket;
using UnityEngine;

namespace DunkShot.Core.Audio
{
    public class SoundManager : MonoBehaviour
    {
        [Header(" Sounds ")]
        [SerializeField]
        private AudioSource _buttonSound;
        [SerializeField]
        private AudioSource _basketHitSound;
        [SerializeField]
        private AudioSource _starCollectSound;
        [SerializeField]
        private AudioSource _shotSound;
        [SerializeField]
        private AudioSource _gameOverSound;

        private void Start()
        {
            GameManager.OnGameStateChanged += GameStateChangedCallback;
            StarHandler.onStarCollect += StarCollect;
            BallTrajectory.onShot += Shot;
            BasketController.onGetIntoBasket += BasketHit;
        }

        private void Shot()
        {
            _shotSound.Play();
        }

        private void StarCollect()
        {
            _starCollectSound.Play();
        }

        private void BasketHit()
        {
            _basketHitSound.Play();
        }

        private void GameStateChangedCallback(GameManager.GAME_STATE gameState)
        {
            if (gameState == GameManager.GAME_STATE.GameOver)
                _gameOverSound.Play();
            else
                _buttonSound.Play();
        }

        public void EnableSounds()
        {
            _buttonSound.volume = 1;
            _basketHitSound.volume = 1;
            _starCollectSound.volume = 1;
            _gameOverSound.volume = 1;
        }

        public void DisableSounds()
        {
            _buttonSound.volume = 0;
            _basketHitSound.volume = 0;
            _starCollectSound.volume = 0;
            _gameOverSound.volume = 0;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= GameStateChangedCallback;
            StarHandler.onStarCollect -= StarCollect;
            BallTrajectory.onShot -= Shot;
            BasketController.onGetIntoBasket -= BasketHit;
        }
    }
}