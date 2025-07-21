using FMOD.Studio;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class T3A_OptionsMenu : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider MasterVolumeSlider;
    [SerializeField] private Slider MusicVolumeSlider;
    [SerializeField] private Slider AmbianceVolumeSlider;
    [SerializeField] private Slider DialogueVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;

    [Header("Dropdowns")]
    [SerializeField] private TMP_Dropdown ScrollDirectionDropdown;
    [SerializeField] private TMP_Dropdown ResolutionDropdown;

    [Header("Toggles")]
    [SerializeField] private Toggle FullscreenToggle;

    private VCA Master;
    private VCA Ambiance;
    private VCA Dialogue;
    private VCA Music;
    private VCA SFX;

    private void Awake()
    {
        Master = RuntimeManager.GetVCA("vca:/Master");
        Ambiance = RuntimeManager.GetVCA("vca:/Ambiance");
        Dialogue = RuntimeManager.GetVCA("vca:/Dialogue");
        Music = RuntimeManager.GetVCA("vca:/Music");
        SFX = RuntimeManager.GetVCA("vca:/SFX");
    }

    // When options menu is opened, the UI elements in it should be updated to reflect the current preferences
    private void OnEnable()
    {
        // Update value of sliders in UI to match preferences
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        AmbianceVolumeSlider.value = PlayerPrefs.GetFloat("AmbianceVolume");
        DialogueVolumeSlider.value = PlayerPrefs.GetFloat("DialogueVolume");
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        // Update value of dropdowns in UI to match preferences
        ScrollDirectionDropdown.value = PlayerPrefs.GetInt("ScrollDirection");
        ResolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        FullscreenToggle.isOn = bool.Parse(PlayerPrefs.GetString("Fullscreen"));
    }

    public void ResetToDefaultVolumes()
    {
        // Reset volume
        UpdateMasterVolume(1f);
        UpdateAmbianceVolume(0.5f);
        UpdateDialogueVolume(0.5f);
        UpdateMusicVolume(0.5f);
        UpdateSFXVolume(0.5f);

        // Update value of sliders in UI to match preferences
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        AmbianceVolumeSlider.value = PlayerPrefs.GetFloat("AmbianceVolume");
        DialogueVolumeSlider.value = PlayerPrefs.GetFloat("DialogueVolume");
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void UpdateMasterVolume(float newValue)
    {
        Master.setVolume(newValue);
        PlayerPrefs.SetFloat("MasterVolume", newValue);
    }

    public void UpdateAmbianceVolume(float newValue)
    {
        Ambiance.setVolume(newValue);
        PlayerPrefs.SetFloat("AmbianceVolume", newValue);
    }

    public void UpdateDialogueVolume(float newValue)
    {
        Dialogue.setVolume(newValue);
        PlayerPrefs.SetFloat("DialogueVolume", newValue);
    }

    public void UpdateMusicVolume(float newValue)
    {
        Music.setVolume(newValue);
        PlayerPrefs.SetFloat("MusicVolume", newValue);
    }

    public void UpdateSFXVolume(float newValue)
    {
        SFX.setVolume(newValue);
        PlayerPrefs.SetFloat("SFXVolume", newValue);
    }

    public void SetScrollDirection(int dropdownIndex)
    {
        PlayerPrefs.SetInt("ScrollDirection", dropdownIndex);
    }

    public void SetResolution(int dropdownIndex)
    {
        // Keep track of if the screen should be fullscreen
        bool isFullscreen = bool.Parse(PlayerPrefs.GetString("Fullscreen"));
        Debug.Log("Resolution/Fullscreen: " + isFullscreen);

        // Update resolution
        if (dropdownIndex == 0)
        {
            Screen.SetResolution(960, 540, isFullscreen);
        }

        if (dropdownIndex == 1)
        {
            Screen.SetResolution(1280, 720, isFullscreen);
        }

        if (dropdownIndex == 2)
        {
            Screen.SetResolution(1920, 1080, isFullscreen);
        }

        PlayerPrefs.SetInt("Resolution", dropdownIndex);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Debug.Log("Fullscreen: " + isFullscreen);
        if (isFullscreen)
        {
            PlayerPrefs.SetString("Fullscreen", "true");
        }
        else
        {
            PlayerPrefs.SetString("Fullscreen", "false");
        }

        // Also update resolution
        SetResolution(PlayerPrefs.GetInt("Resolution"));
    }
}
