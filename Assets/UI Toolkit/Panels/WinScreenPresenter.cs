using System;
using UnityEngine;
using UnityEngine.UIElements;

public class WinScreenPresenter
{
    public Action QuitAction { set => _mainMenuButton.clicked += value; }

    Button _mainMenuButton;

    public WinScreenPresenter(VisualElement root)
    {
        _mainMenuButton = root.Q<Button>("MainMenuButton");
    }
}
