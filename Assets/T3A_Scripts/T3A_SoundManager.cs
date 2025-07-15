using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;

public class T3A_SoundManager : MonoBehaviour
{
    // Make it so that the DoNotDestroy gameobject (and all of its children, including the options menu and music) persist between scenes
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    /*
    FMOD.Studio.VCA Master;
    FMOD.Studio.VCA SFX;
    FMOD.Studio.VCA Dialogue;
    FMOD.Studio.VCA Music;

    float MasterVolume = 1f;
    float SFXVolume = 0.5f;
    float DialogueVolume = 0.5f;
    float MusicVolume = 0.5f;
    */
    private void Start()
    {
        //StartCoroutine(GetVCAs());
    }

    /*
    private void Update()
    {
        Master.setVolume(MasterVolume);
        SFX.setVolume(SFXVolume);
        Dialogue.setVolume(DialogueVolume);
        Music.setVolume(MusicVolume);
    }
    

    IEnumerator GetVCAs()
    {
        // Wait for 1 sec before calling .GetVCA (https://qa.fmod.com/t/cant-access-fmod-unity-vca-c/19439)
        yield return new WaitForSeconds(1);

        Master = RuntimeManager.GetVCA("vca:/Master");
        SFX = RuntimeManager.GetVCA("vca:/SFX");
        Dialogue = RuntimeManager.GetVCA("vca:/Dialogue");
        Music = RuntimeManager.GetVCA("vca:/Music");
    }
    */

}
