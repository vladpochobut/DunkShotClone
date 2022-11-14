using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GAME_STATE { Menu, Game, GameOver, Settings }

    private GAME_STATE _gameState;

    public static Action<GAME_STATE> OnGameStateChanged;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void SetGameState(GAME_STATE gameState)
    {
        _gameState = gameState;
        OnGameStateChanged?.Invoke(gameState);
    }

    public bool IsGameState()
    {
        return _gameState == GAME_STATE.Game;
    }
}
