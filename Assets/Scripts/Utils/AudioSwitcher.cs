using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioSwitcher : MonoBehaviour
{
    #region Singleton
    public static AudioSwitcher instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    #endregion


    // Audio source
    public AudioSource audioSource;

    // Clips
    public List<AudioClip> clips;

    // Main clip
    public AudioClip mainClip;


    // Switch controller
    private bool hasSwitched = false;
    private bool hasComeFromMainMenu = true;


    // Update is called once per frame
    void Update()
    {
        // If have already switched, exit
        if(hasSwitched || hasComeFromMainMenu)
        {
            return;
        }

        // If the scene is Battle, switch song
        if(SceneManager.GetActiveScene().name == "Battle")
        {
            hasSwitched = true;
            SwitchSong();
        }

        // If it is MainScene, switch back to main clip
        else
        {
            hasSwitched = true;
            audioSource.clip = mainClip;
            audioSource.Play();
        }
    }

    // Called when a new level is loaded
    void OnLevelWasLoaded(int level)
    {
        // If it is the first time to enter on Battle scene, set hasComeFromMainMenu to false and allow to switch sound
        if(SceneManager.GetActiveScene().name == "Battle" && hasComeFromMainMenu)
        {
            hasSwitched = false;
            hasComeFromMainMenu = false;
        }

        // If hasComeFromMainMenu is false, allow to switch sound
        else if(!hasComeFromMainMenu)
        {
            hasSwitched = false;
        }

        // If loaded scene is MainMenu, play main clip and set hasComeFromMainMenu to true
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            audioSource.clip = mainClip;
            audioSource.Play();
            hasComeFromMainMenu = true;
        }
    }

    /// <summary>
    /// Switch main song to a random song.
    /// </summary>
    private void SwitchSong()
    {
        int index = UnityEngine.Random.Range(0, clips.Count);
        audioSource.clip = clips[index];
        audioSource.Play();
    }

    /// <summary>
    /// Pause game audio.
    /// </summary>
    public void PauseSong()
    {
        audioSource.Pause();
    }
}
