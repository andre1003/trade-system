using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    #region Singleton
    public static GameOver instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    #endregion

    // Game over canvas
    public Canvas gameOverCanvas;

    // Audio source
    public AudioSource audioSource;


    /// <summary>
    /// End this game.
    /// </summary>
    public void EndGame()
    {
        // Pause music
        AudioSwitcher.instance.PauseSong();

        // Display game over and play game over sound
        gameOverCanvas.enabled = true;
        audioSource.Play();

        // Destroy player
        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }

    /// <summary>
    /// Go to main menu.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Exit game to desktop.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
