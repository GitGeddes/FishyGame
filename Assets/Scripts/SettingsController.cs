using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public static SettingsController instance;

    public List<string> resolutions = new List<string>()
    {
        "2560x1440",
        "1920x1080",
        "1600x900",
        "1366x768",
        "1280x720"
    };
    public bool isFullscreen = true;
    public string currentResolution = "1920x1080";

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetFullscreen(bool enabled)
    {
        isFullscreen = enabled;
        Screen.fullScreen = enabled;
    }

    public void SetResolution(string newResolution, bool fullscreenEnabled)
    {
        string[] resArray = newResolution.Split('x');
        int[] valuesArray = new int[] { int.Parse(resArray[0]), int.Parse(resArray[1]) };

        Screen.SetResolution(valuesArray[0], valuesArray[1], fullscreenEnabled);
        currentResolution = newResolution;
    }
}
