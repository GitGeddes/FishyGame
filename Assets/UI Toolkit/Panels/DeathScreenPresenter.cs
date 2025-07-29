using System;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathScreenPresenter : MonoBehaviour
{
    public Action QuitAction { set => _mainMenuButton.clicked += value; }

    Button _mainMenuButton;

    public DeathScreenPresenter(VisualElement root)
    {
        _mainMenuButton = root.Q<Button>("MainMenuButton");
    }
}
