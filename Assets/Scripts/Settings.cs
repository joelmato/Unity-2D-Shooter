using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Settings : MonoBehaviour
{
    public GameObject mainMenu;
    public TMP_Dropdown resolutionDropdown;
    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider musicSlider;

    Resolution[] screenResolutions;

    public AudioMixer audioMixer;

    private float masterVolume;
    private float sfxVolume;
    private float musicVolume;

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

        UpdateMasterSlider();
        UpdateSFXSlider();
        UpdateMusicSlider();
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

    public void SetMasterVolume(float decimalVolume)
    {

        audioMixer.SetFloat("VolumeMaster", DecimalToDB(decimalVolume));
    }

    public void SetSFXVolume(float decimalVolume)
    {
        audioMixer.SetFloat("VolumeSFX", DecimalToDB(decimalVolume));
    }

    public void SetMusicVolume(float decimalVolume)
    {
        audioMixer.SetFloat("VolumeMusic", DecimalToDB(decimalVolume));
    }

    private float DecimalToDB(float decimalValue)
    {
        var dbValue = Mathf.Log10(decimalValue) * 20;

        if (decimalValue == 0)
        {
            dbValue = -80.0f;
        }

        return dbValue;
    }

    private float DBToDecimal(float dbValue)
    {
        var decimalValue = Mathf.Pow(10, dbValue / 20);

        if (dbValue == -80.0f)
        {
            decimalValue = 0.0f;
        }

        return decimalValue;
    }

    private void UpdateMasterSlider()
    {
        bool value = audioMixer.GetFloat("VolumeMaster", out masterVolume);
        if (value)
        {
            masterSlider.value = DBToDecimal(masterVolume);
        }
        else
        {
            masterSlider.value = 1f;
        }
    }

    private void UpdateSFXSlider()
    {
        bool value = audioMixer.GetFloat("VolumeSFX", out sfxVolume);
        if (value)
        {
            float test = DBToDecimal(sfxVolume);
            sfxSlider.value = test;
        }
        else
        {
            sfxSlider.value = 1f;
        }
    }

    private void UpdateMusicSlider()
    {
        bool value = audioMixer.GetFloat("VolumeMusic", out musicVolume);
        if (value)
        {
            musicSlider.value = DBToDecimal(musicVolume);
        }
        else
        {
            musicSlider.value = 1f;
        }
    }
}
