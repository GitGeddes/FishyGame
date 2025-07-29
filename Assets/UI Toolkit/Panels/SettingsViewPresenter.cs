using System;
using UnityEngine.UIElements;

public class SettingsViewPresenter
{
    public Action BackAction { set => _backButton.clicked += value; }

    private Button _backButton;
    private Toggle _fullscreenToggle;
    private DropdownField _resolutionDropdown;

    public SettingsViewPresenter(VisualElement root) {
        _backButton = root.Q<Button>("BackButton");

        _fullscreenToggle = root.Q<Toggle>("FullscreenToggle");
        _fullscreenToggle.RegisterCallback<MouseUpEvent>((evt) => SetFullscreen(!_fullscreenToggle.value), TrickleDown.TrickleDown);
        _fullscreenToggle.value = SettingsController.instance.isFullscreen;
        SetFullscreen(SettingsController.instance.isFullscreen);

        _resolutionDropdown = root.Q<DropdownField>("ResolutionDropdown");
        _resolutionDropdown.choices = SettingsController.instance.resolutions;
        _resolutionDropdown.RegisterValueChangedCallback((value) => SetResolution(value.newValue));
        _resolutionDropdown.index = SettingsController.instance.resolutions.IndexOf(SettingsController.instance.currentResolution);
        SetResolution(SettingsController.instance.currentResolution);
    }

    private void SetFullscreen(bool enabled)
    {
        SettingsController.instance.SetFullscreen(enabled);
    }

    private void SetResolution(string newResolution)
    {
        SettingsController.instance.SetResolution(newResolution, _fullscreenToggle.value);
    }
}
