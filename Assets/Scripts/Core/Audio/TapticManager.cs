using DunkShot.Core.Ball;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void TapticEnabledSwitch()
    {
        if (_isTapticEnabled)
            DisableVibration();
        else
            EnableVibration();
    }

    private void EnableVibration()
    {
        _haptics = true;
    }

    private void DisableVibration()
    {
        _haptics = false;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallback;
        StarHandler.onStarCollect -= Vibrate;
        BallTrajectory.onShot -= Vibrate;
    }
}
