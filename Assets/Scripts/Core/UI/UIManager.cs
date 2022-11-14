using Assets.Scripts.Core.Points;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField]
    private GameObject _menuPanel;
    [SerializeField]
    private GameObject _gamePanel;
    [SerializeField]
    private GameObject _gameOverPanel;
    [SerializeField]
    private GameObject _pausePanel;
    [SerializeField]
    private GameObject _settingsPanel;
    [SerializeField]
    private Text _starsCountText;
    [SerializeField]
    private GameObject _gameplay;

    private void Start()
    {
        _menuPanel.SetActive(true);
        _gamePanel.SetActive(false);
        _gameOverPanel.SetActive(false);
        _settingsPanel.SetActive(false);
        _pausePanel.SetActive(false);
        _gameplay.SetActive(false);
        _starsCountText.text = $"{PointsController.Instance.GetStarsCount()}";
        GameManager.OnGameStateChanged += GameStateChangeCallback;
    }

    public void PlayButtonPressed()
    {
        GameManager.Instance.SetGameState(GameManager.GAME_STATE.Game);
        _menuPanel.SetActive(false);
        _gamePanel.SetActive(true);
        _gameplay.SetActive(true);
    }

    public void SettingsPanelButtonPressed()
    {
        _settingsPanel.SetActive(!_settingsPanel.activeSelf);
    }

    public void PauseMenuBattonPressed()
    {
        Time.timeScale = _pausePanel.activeSelf == true ? 0 : 1;
        _pausePanel.SetActive(!_pausePanel.activeSelf);
    }

    public void MainMenuButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GameStateChangeCallback(GameManager.GAME_STATE gameState)
    {
        if (gameState == GameManager.GAME_STATE.GameOver)
            ShowGameOverPanel();
    }

    public void ShowGameOverPanel()
    {
        if (_gameOverPanel == null) return;
        _gameOverPanel.SetActive(true);
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameStateChangeCallback;
    }
}
