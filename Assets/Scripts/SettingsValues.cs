using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SettingsValues : MonoBehaviour
{
    public static float sensitivity = 1f;
    public static float verticalSens = 1f;
    public static float horizontalSens = 1f;

    // New settings variables
    public static int resolutionIndex = 0;
    public static float masterVolume = 1f;
    public static bool vsyncEnabled = true;
    public static int frameRateCap = 60;
    public static float gammaValue = 1f;

    public static float highScore = 0f;

    public static bool initialLoad = true;

    private string settingsFilePath;

    private void Start()
    {
        // Define the path to the settings file
        settingsFilePath = Path.Combine(Application.persistentDataPath, "settings.json");

        // Load settings from the file on start
        LoadSettings();
        initialLoad = false;

    }

    // Save settings to a file
    public void SaveSettings()
    {
        SettingsData settingsData = new()
        {
            Sensitivity = sensitivity,
            VerticalSens = verticalSens,
            HorizontalSens = horizontalSens,
            ResolutionIndex = resolutionIndex,
            MasterVolume = masterVolume,
            VsyncEnabled = vsyncEnabled,
            FrameRateCap = frameRateCap,
            GammaValue = gammaValue,
            HighScore = highScore
        };

        string json = JsonUtility.ToJson(settingsData);
        File.WriteAllText(settingsFilePath, json);
    }

    // Load settings from a file
    public void LoadSettings()
    {
        if (initialLoad)
        {

            if (File.Exists(settingsFilePath))
            {
                string json = File.ReadAllText(settingsFilePath);
                SettingsData settingsData = JsonUtility.FromJson<SettingsData>(json);

                sensitivity = settingsData.Sensitivity;
                verticalSens = settingsData.VerticalSens;
                horizontalSens = settingsData.HorizontalSens;
                resolutionIndex = settingsData.ResolutionIndex;
                masterVolume = settingsData.MasterVolume;
                vsyncEnabled = settingsData.VsyncEnabled;
                frameRateCap = settingsData.FrameRateCap;
                gammaValue = settingsData.GammaValue;
                highScore = settingsData.HighScore;

                ApplySettings();
            }
            if (!File.Exists(settingsFilePath))
            {
                sensitivity = 1f;
                verticalSens = 1f;
                horizontalSens = 1f;
                resolutionIndex = 0;
                masterVolume = 1f;
                vsyncEnabled = true;
                frameRateCap = 60;
                gammaValue = 1f;
                highScore = 0f;
            }
        }
    }

    // Apply settings to the game
    public void ApplySettings()
    {
        // Apply resolution change
        ApplyResolution();

        // Apply other settings (volume, vsync, frame rate cap, gamma)
        AudioListener.volume = masterVolume;
        QualitySettings.vSyncCount = vsyncEnabled ? 1 : 0;
        Application.targetFrameRate = frameRateCap;
        RenderSettings.ambientLight = new Color(gammaValue, gammaValue, gammaValue);

    }

    // Apply resolution change
    private void ApplyResolution()
    {
        Resolution[] resolutions = Screen.resolutions;

        if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
        {
            Resolution selectedResolution = resolutions[resolutionIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }
    }

    // New method for changing sensitivity
    public void ChangeSensitivity(float newSensitivity)
    {
        sensitivity = newSensitivity;
        SaveSettings(); // Save settings after changing sensitivity
    }

    // New method for changing volume
    public void ChangeVolume(float newVolume)
    {
        masterVolume = newVolume;
        SaveSettings(); // Save settings after changing volume
    }

    // New method for toggling Vsync
    public void ToggleVsync()
    {
        vsyncEnabled = !vsyncEnabled;
        SaveSettings();
    }

    // New method for changing frame rate cap
    public void ChangeFrameRateCap(float newFrameRateCap)
    {
        frameRateCap = Mathf.RoundToInt(newFrameRateCap);
        SaveSettings();
    }

    // New method for changing gamma
    public void ChangeGamma(float newGamma)
    {
        gammaValue = newGamma;
        SaveSettings();
    }

    // New method for changing resolution
    public void ChangeResolution(int newResolutionIndex)
    {
        resolutionIndex = newResolutionIndex;
        SaveSettings();
    }

    // Called when the application quits
    public void OnApplicationQuit()
    {
        SaveSettings();
    }


    public List<string> GetResolutionOptions()
    {
        List<string> options = new();
        Resolution[] resolutions = Screen.resolutions;

        foreach (Resolution resolution in resolutions)
        {
            options.Add($"{resolution.width}x{resolution.height} @ {resolution.refreshRateRatio}");
        }

        return options;
    }


}

[System.Serializable]
public class SettingsData
{
    public float Sensitivity;
    public float VerticalSens;
    public float HorizontalSens;
    public int ResolutionIndex;
    public float MasterVolume;
    public bool VsyncEnabled;
    public int FrameRateCap;
    public float GammaValue;
    public float HighScore;
}
