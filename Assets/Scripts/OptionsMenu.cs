// OptionsMenu.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public SettingsValues settingsValues;

    public AudioSource audioSource;
    public AudioClip near;

    public Slider sensitivitySlider;
    public Slider volumeSlider;
    public Toggle vsyncToggle;
    public Slider frameRateCapSlider;
    public Slider gammaSlider;
    public TMP_Dropdown resolutionDropdown;
    public Button applyButton;
    public Button exitButton;

    public TextMeshProUGUI sensitivityText;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI frameRateCapText;
    public TextMeshProUGUI gammaText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        InitializeUI();
        settingsValues.ApplySettings(); // Apply settings when the game starts
    }


    void InitializeUI()
    {
        frameRateCapSlider.minValue = 30f;
        frameRateCapSlider.maxValue = 999f;

        sensitivitySlider.minValue = 0.01f;
        sensitivitySlider.maxValue = 5f;

        // Initialize UI elements and set their values based on settingsValues
        sensitivitySlider.value = SettingsValues.sensitivity;
        volumeSlider.value = SettingsValues.masterVolume;
        vsyncToggle.isOn = SettingsValues.vsyncEnabled;
        frameRateCapSlider.value = SettingsValues.frameRateCap;
        gammaSlider.value = SettingsValues.gammaValue;

        // Set the dropdown options and highlight the current resolution
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(settingsValues.GetResolutionOptions());
        resolutionDropdown.value = SettingsValues.resolutionIndex;

        // Register callbacks for slider value changes
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        frameRateCapSlider.onValueChanged.AddListener(OnFrameRateCapChanged);
        gammaSlider.onValueChanged.AddListener(OnGammaChanged);

        // Update the UI text
        UpdateUIText();
    }

    void OnSensitivityChanged(float value)
    {
        SettingsValues.sensitivity = value;
        UpdateUIText();
    }

    void OnVolumeChanged(float value)
    {
        SettingsValues.masterVolume = value;
        UpdateUIText();
    }

    void OnFrameRateCapChanged(float value)
    {
        SettingsValues.frameRateCap = Mathf.RoundToInt(value);
        UpdateUIText();
    }

    void OnGammaChanged(float value)
    {
        SettingsValues.gammaValue = value;
        UpdateUIText();
    }

    void UpdateUIText()
    {
        // Update the text of UI elements based on current settings
        sensitivityText.text = $"Sensitivity: {SettingsValues.sensitivity}";
        volumeText.text = $"Volume: {Mathf.RoundToInt(SettingsValues.masterVolume * 100)}";
        frameRateCapText.text = $"Frame Rate Cap: {SettingsValues.frameRateCap}";
        gammaText.text = $"Gamma: {SettingsValues.gammaValue}";
    }

    public void ApplySettings()
    {
        settingsValues.ApplySettings();
        audioSource.PlayOneShot(near);
        
    }

    public void ExitMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
