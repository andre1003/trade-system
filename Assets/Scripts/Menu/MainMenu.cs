using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    // Load progress bar
    public Slider progressBar;

    // Main menu and load canvas
    public Canvas mainMenu;
    public Canvas load;


    /// <summary>
    /// Play game.
    /// </summary>
    public void Play()
    {
        StartCoroutine(LoadScene());
        mainMenu.enabled = false;
        load.enabled = true;
    }

    /// <summary>
    /// Exit game.
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }


    /// <summary>
    /// Load level async.
    /// </summary>
    private IEnumerator LoadScene()
    {
        // Start async load
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainScene");

        // While not loaded
        while(!asyncLoad.isDone)
        {
            // Display load progress
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progressBar.value = progress;

            // Return null
            yield return null;
        }

    }
}
