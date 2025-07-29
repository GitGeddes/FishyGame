using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuPresenter
{
    public Action OpenSettings { set => _settingsButton.clicked += value; }

    private Button _playButton;
    private Button _settingsButton;
    private Button _quitButton;

    public MainMenuPresenter(VisualElement root)
    {
        _playButton = root.Q<Button>("PlayButton");
        _settingsButton = root.Q<Button>("SettingsButton");
        _quitButton = root.Q<Button>("QuitButton");

        AddCallbacksToButtons();
    }

    private void AddCallbacksToButtons()
    {
        _playButton.clicked += () => StartGame();
        _quitButton.clicked += () => QuitGame();
    }

    private void StartGame()
    {
        Debug.Log("loading PlayScene");
        SceneManager.LoadScene("PlayScene");
    }

    private void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
