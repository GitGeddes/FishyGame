using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MainViewPresenter : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _playerInput;

    private VisualElement _mainMenuView;
    private VisualElement _settingsView;

    void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _mainMenuView = root.Q("MainMenuView");
        _settingsView = root.Q("SettingsView");

        MainMenuPresenter menuPresenter = new(_mainMenuView)
        {
            OpenSettings = () =>
            {
                _mainMenuView.style.display = DisplayStyle.None;
                _settingsView.style.display = DisplayStyle.Flex;
                _playerInput.enabled = false;
            }
        };

        SettingsViewPresenter settingsPresenter = new(_settingsView)
        {
            BackAction = () =>
            {
                _mainMenuView.style.display = DisplayStyle.Flex;
                _settingsView.style.display = DisplayStyle.None;
                _playerInput.enabled = true;
            }
        };
    }
}
