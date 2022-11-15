using DunkShot.Core.Ball;
using DunkShot.Core.Basket;
using UnityEngine;

namespace DunkShot.Core.Audio
{
    public class TapticManager : MonoBehaviour
    {
        [Header(" Settings ")]
        private bool _haptics;

        private bool _isTapticEnabled;
        private void Start()
        {
            GameManager.OnGameStateChanged += GameStateChangedCallback;
            StarHandler.onStarCollect += Vibrate;
            BallTrajectory.onShot += Vibrate;
            BasketController.onGetIntoBasket += Vibrate;
        }

        private void Vibrate()
        {
            if (_haptics)
                Taptic.Light();
        }

        private void GameStateChangedCallback(GameManager.GAME_STATE gameState)
        {
            if (gameState == GameManager.GAME_STATE.GameOver)
                Vibrate();
        }

        public void EnableVibration()
        {
            _haptics = true;
        }

        public void DisableVibration()
        {
            _haptics = false;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= GameStateChangedCallback;
            StarHandler.onStarCollect -= Vibrate;
            BallTrajectory.onShot -= Vibrate;
            BasketController.onGetIntoBasket -= Vibrate;
        }
    }
}
