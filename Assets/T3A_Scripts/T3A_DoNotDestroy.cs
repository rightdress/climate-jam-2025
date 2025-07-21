using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class T3A_DoNotDestroy : MonoBehaviour
{
    private VCA Master;
    private VCA Ambiance;
    private VCA Dialogue;
    private VCA Music;
    private VCA SFX;

    // Make it so that the DoNotDestroy gameobject (and all of its children, including the music) persist between scenes
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        // Set audio levels properly as soon as game opens
        Master = RuntimeManager.GetVCA("vca:/Master");
        Ambiance = RuntimeManager.GetVCA("vca:/Ambiance");
        Dialogue = RuntimeManager.GetVCA("vca:/Dialogue");
        Music = RuntimeManager.GetVCA("vca:/Music");
        SFX = RuntimeManager.GetVCA("vca:/SFX");

        // Set inital volume levels if they don't already exist in player prefs
        if (!PlayerPrefs.HasKey("MasterVolume")) UpdateMasterVolume(1f);
        else UpdateMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));

        if (!PlayerPrefs.HasKey("AmbianceVolume")) UpdateAmbianceVolume(0.5f);
        else UpdateAmbianceVolume(PlayerPrefs.GetFloat("AmbianceVolume"));

        if (!PlayerPrefs.HasKey("DialogueVolume")) UpdateDialogueVolume(0.5f);
        else UpdateDialogueVolume(PlayerPrefs.GetFloat("DialogueVolume"));

        if (!PlayerPrefs.HasKey("MusicVolume")) UpdateMusicVolume(0.5f);
        else UpdateMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));

        if (!PlayerPrefs.HasKey("SFXVolume")) UpdateSFXVolume(0.5f);
        else UpdateSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));

        // Set other inital preferences if they don't already exist in player prefs
        if (!PlayerPrefs.HasKey("ScrollDirection"))
        {
            SetScrollDirection(0);
        }
        if (!PlayerPrefs.HasKey("Fullscreen")) // Set up fullscreen before resolution
        {
            SetFullScreen(false);
        }
        if (!PlayerPrefs.HasKey("Resolution"))
        {
            SetResolution(0);
        }
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
