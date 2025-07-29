using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayViewPresenter : MonoBehaviour
{
    private VisualElement _playHUD;
    private VisualElement _pauseView;
    private VisualElement _settingsView;
    private VisualElement _winScreen;
    private VisualElement _deathScreen;
    private bool isPaused = false;

    void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _playHUD = root.Q("HUD");
        _pauseView = root.Q("PauseView");
        _settingsView = root.Q("SettingsView");
        _winScreen = root.Q("WinScreen");
        _deathScreen = root.Q("DeathScreen");

        PauseViewPresenter pausePresenter = new(_pauseView)
        {
            ResumeClicked = () => Resume(),
            SettingsClicked = () => OpenSettings(),
            QuitClicked = () => Quit()
        };

        SettingsViewPresenter settingsViewPresenter = new(_settingsView)
        {
            BackAction = () => CloseSettings()
        };

        WinScreenPresenter winScreenPresenter = new(_winScreen)
        {
            QuitAction = () => Quit()
        };

        DeathScreenPresenter deathScreenPresenter = new(_deathScreen)
        {
            QuitAction = () => Quit()
        };
    }

    void Update()
    {
        if (!isPaused && Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
        else if (isPaused && Input.GetKeyUp(KeyCode.Escape))
        {
            Resume();
        }
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        _playHUD.style.display = DisplayStyle.Flex;
        _pauseView.style.display = DisplayStyle.None;
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        _playHUD.style.display = DisplayStyle.None;
        _pauseView.style.display = DisplayStyle.Flex;
    }
    
    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OpenSettings()
    {
        _pauseView.style.display = DisplayStyle.None;
        _settingsView.style.display = DisplayStyle.Flex;
    }

    public void CloseSettings()
    {
        _pauseView.style.display = DisplayStyle.Flex;
        _settingsView.style.display = DisplayStyle.None;
    }

    public void Win()
    {
        isPaused = true;
        Time.timeScale = 0;
        _playHUD.style.display = DisplayStyle.None;
        _winScreen.style.display = DisplayStyle.Flex;
    }

    public void Lose()
    {
        isPaused = true;
        Time.timeScale = 0;
        _playHUD.style.display = DisplayStyle.None;
        _deathScreen.style.display = DisplayStyle.Flex;
    }
}
