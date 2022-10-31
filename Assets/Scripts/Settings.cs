using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject mainMenu;
    public TMP_Dropdown resolutionDropdown;

    Resolution[] screenResolutions;

    void Start()
    {
        screenResolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        // Fills the resolution dropdown with all possible resolutions for the system the game is running on
        List<string> options = new List<string>();
        int index = 0;
        for (int i = 0; i < screenResolutions.Length; i++)
        {
            string option = screenResolutions[i].width + "x" + screenResolutions[i].height + " " + screenResolutions[i].refreshRate + "Hz";
            options.Add(option);

            if (screenResolutions[i].width == Screen.width && screenResolutions[i].height == Screen.height && screenResolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                index = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = index;
        resolutionDropdown.RefreshShownValue();

    }

    public void BackFromSettings()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void SetResolution(int index)
    {
        Resolution newResolution = screenResolutions[index];
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
    }
}
