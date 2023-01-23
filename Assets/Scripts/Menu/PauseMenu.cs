using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    #region Singleton
    public static PauseMenu instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    #endregion

    // Pause menu canvas
    public Canvas pauseMenuCanvas;

    // Is game paused?
    public bool isGamePaused = false;


    /// <summary>
    /// Pause the game.
    /// </summary>
    public void Pause() 
    {
        pauseMenuCanvas.enabled = true;
        Time.timeScale = 0f;
        isGamePaused = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>().SetIsBusy(true);
    }

    /// <summary>
    /// Resume the game.
    /// </summary>
    public void Resume()
    {
        pauseMenuCanvas.enabled = false;
        Time.timeScale = 1f;
        isGamePaused = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>().SetIsBusy(false);
    }

    /// <summary>
    /// Load main menu scene.
    /// </summary>
    public void MainMenu()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        StartCoroutine(LoadMainMenu());
    }

    /// <summary>
    /// Load main menu scene async.
    /// </summary>
    private IEnumerator LoadMainMenu()
    {
        AsyncOperation asynLoad = SceneManager.LoadSceneAsync("MainMenu");

        while(!asynLoad.isDone)
        {
            yield return null;
        }
    }
}
