using System;
using UnityEngine.UIElements;

public class PauseViewPresenter
{
    public Action ResumeClicked { set => _resumeButton.clicked += value; }
    public Action SettingsClicked { set => _settingsButton.clicked += value; }
    public Action QuitClicked { set => _quitButton.clicked += value; }

    Button _resumeButton;
    Button _settingsButton;
    Button _quitButton;

    public PauseViewPresenter(VisualElement root)
    {
        _resumeButton = root.Q<Button>("ResumeButton");
        _settingsButton = root.Q<Button>("SettingsButton");
        _quitButton = root.Q<Button>("QuitButton");
    }
}
